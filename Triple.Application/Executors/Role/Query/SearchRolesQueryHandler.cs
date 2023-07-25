using Triple.Application.Dtos.Role;
using Triple.Application.Queries.Role;
using Triple.Application.Shared;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Executors.Role.Query
{
    public class SearchRolesQueryHandler : IRequestHandler<SearchRolesQuery, QueryResultOfList<RoleDto>>
    {
        private TripleDbContext _dbContext;

        public SearchRolesQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueryResultOfList<RoleDto>> Handle(SearchRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await (from role in _dbContext.Roles
                               select new RoleDto()
                               {
                                   Id = role.Id,
                                   Name = role.Name,
                                   Description = role.Description
                               }).ToListAsync();

            if (!string.IsNullOrEmpty(request.Search))
                roles = roles.Where(u => u.Name.ToUpper().Contains(request.Search.ToUpper()) ||
                                         u.Description.ToUpper().Contains(request.Search.ToUpper())).ToList();

            var result = roles.AsQueryable().ToPaging(request.Page, request.PageSize).ToList();

            return new QueryResultOfList<RoleDto>
            {
                Records = result.ToList(),
                Total = roles.Count()
            };
        }
    }
}
