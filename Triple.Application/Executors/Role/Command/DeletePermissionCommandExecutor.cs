using Triple.Application.Commands.Role;
using Triple.Infrastructure.Identity;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Resources;
using Triple.Shared.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Executors.Role.Command
{
    public class DeletePermissionCommandExecutor : Executor, IRequestHandler<DeletePermissionCommand, Result>
    {
        private readonly RoleManager<UserRole> _roleManager;
        private TripleDbContext _db;

        public DeletePermissionCommandExecutor(TripleDbContext db, RoleManager<UserRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
        }

        public async Task<Result> Handle(DeletePermissionCommand request, CancellationToken cancellationToken)
        {
            var role = await _db.Roles.Include(r => r.Permissions).FirstOrDefaultAsync(r => r.Id == request.RoleId);

            if (role is null)
                return NotFound();

            var permission = role.Permissions.FirstOrDefault(p => p.PermissionId == request.PermissionId);

            if (permission is null)
                return NotFound();

            role.RemovePermission(permission);

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                return Failed(ApplicationStrings.RoleUpdateFailed);

            return Succeeded(role.Id);
        }
    }
}
