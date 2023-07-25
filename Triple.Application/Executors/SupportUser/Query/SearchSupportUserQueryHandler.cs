using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.SupportUSer;
using Triple.Application.Queries.SupportUser;
using Triple.Application.Shared;
using Triple.Infrastructure.Persistence;
using Triple.Shared;

namespace Triple.Application.Executors.SupportUser.Query
{
    public class SearchSupportUserQueryHandler : IRequestHandler<SearchSupportUserQuery, QueryResultOfList<SupportUserDto>>
    {
        private TripleDbContext _dbContext;

        public SearchSupportUserQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueryResultOfList<SupportUserDto>> Handle(SearchSupportUserQuery request, CancellationToken cancellationToken)
        {
            var users = await (from user in _dbContext.SupportUsers
                               select new SupportUserDto()
                               {
                                   Email = user.Email,
                                   EntityId = user.EntityId,
                                   FirstName = user.FirstName,
                                   LastName = user.LastName
                               }).ToListAsync();

            var result = users.AsQueryable().FilterAndSort(request.Filters, request.Sortings).ToPaging(request.Page, request.PageSize).ToList();

            return new QueryResultOfList<SupportUserDto>
            {
                Records = result,
                Total = users.Count
            };
        }
    }
}
