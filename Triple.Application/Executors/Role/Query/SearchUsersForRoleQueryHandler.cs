using Triple.Application.Dtos.User;
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
    public class SearchUsersForRoleQueryHandler : IRequestHandler<SearchUsersForRoleQuery, QueryResultOfList<UserDto>>
    {
        private TripleDbContext _dbContext;

        public SearchUsersForRoleQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueryResultOfList<UserDto>> Handle(SearchUsersForRoleQuery request, CancellationToken cancellationToken)
        {
            var roleUserIds = await (from user in _dbContext.Users
                                     from role in user.Roles
                                     where role.RoleId == request.RoleId
                                     select user.Id).ToListAsync();

            var users = await (from user in _dbContext.Users.Where(u => !roleUserIds.Contains(u.Id))
                               select new UserDto
                               {
                                   Id = user.Id,
                                   Email = user.Email,
                                   PhoneNumber = user.PhoneNumber,
                                   UserName = user.UserName
                               }).ToListAsync();

            if (!string.IsNullOrEmpty(request.Name))
                users = users.Where(x => x.Email.ToUpper().Contains(request.Name.ToUpper()) ||
                                         x.UserName.ToUpper().Contains(request.Name.ToUpper())).ToList();

            var result = users.AsQueryable().ToPaging(request.Page, request.PageSize);

            return new QueryResultOfList<UserDto>()
            {
                Records = result.ToList(),
                Total = users.AsQueryable().Count()
            };
        }
    }
}
