using Triple.Application.Dtos.Role;
using Triple.Application.Dtos.User;
using Triple.Application.Queries.Role;
using Triple.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Executors.Role.Query
{
    public class GetRoleDetailsQueryHandler : IRequestHandler<GetRoleDetailsQuery, RoleDetailsDto?>
    {
        private TripleDbContext _dbContext;

        public GetRoleDetailsQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RoleDetailsDto?> Handle(GetRoleDetailsQuery request, CancellationToken cancellationToken)
        {
            var users = await (from user in _dbContext.Users
                               from role in user.Roles
                               where role.RoleId == request.RoleId
                               select new UserDto
                               {
                                   Id = user.Id,
                                   UserName = user.UserName,
                                   Email = user.Email,
                                   PhoneNumber = user.PhoneNumber
                               }).ToListAsync();

            return await (from role in _dbContext.Roles
                          where role.Id == request.RoleId
                          let permissions = from permission in role.Permissions
                                            select new PermissionDto
                                            {
                                                Id = permission.PermissionId,
                                                PermissionKey = permission.PermissionKey,
                                                Description = permission.Description
                                            }
                          select new RoleDetailsDto
                          {
                              Role = new RoleDto
                              {
                                  Id = role.Id,
                                  Name = role.Name,
                                  Description = role.Description
                              },
                              Permissions = permissions.ToList(),
                              Users = users.ToList()
                          }).FirstOrDefaultAsync();
        }
    }
}
