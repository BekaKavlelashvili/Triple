using Triple.Application.Commands.Role;
using Triple.Infrastructure.Identity;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Resources;
using Triple.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Executors.Role.Command
{
    public class SetPermissionCommandExecutor : Executor, IRequestHandler<SetPermissionCommand, Result>
    {
        private readonly RoleManager<UserRole> _roleManager;
        private TripleDbContext _db;

        public SetPermissionCommandExecutor(TripleDbContext db, RoleManager<UserRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
        }

        public async Task<Result> Handle(SetPermissionCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);

            if (role is null)
                return NotFound();

            role.SetPermissions(request.Permissions.ConvertAll(p => new Permission()
            {
                Id = p.Id,
                PermissionKey = p.PermissionKey,
                Description = p.Description
            }));

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                return Failed(ApplicationStrings.RoleUpdateFailed);

            return Succeeded(role.Id);
        }
    }
}
