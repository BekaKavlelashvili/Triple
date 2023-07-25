using Triple.Application.Dtos.Role;
using Triple.Application.Queries.Role;
using Triple.Application.Shared;
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
    public class GetPermissionsQueryHandler : IRequestHandler<GetPermissionsQuery, List<PermissionGroupDto>>
    {
        private TripleDbContext _dbContext;

        public GetPermissionsQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PermissionGroupDto>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            return await (from gr in _dbContext.PermissionGroups
                          let permissions = from permission in gr.Permissions
                                            select new PermissionDto
                                            {
                                                Id = permission.Id,
                                                PermissionKey = permission.PermissionKey,
                                                Description = permission.Description
                                            }
                          select new PermissionGroupDto
                          {
                              Id = gr.Id,
                              Name = gr.GroupName,
                              Permissions = permissions.ToList()
                          }).ToListAsync();
        }
    }
}
