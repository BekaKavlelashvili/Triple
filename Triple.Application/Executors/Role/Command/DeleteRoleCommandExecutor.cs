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
    public class DeleteRoleCommandExecutor : Executor, IRequestHandler<DeleteRoleCommand, Result>
    {
        private readonly RoleManager<UserRole> _roleManager;
        private TripleDbContext _db;

        public DeleteRoleCommandExecutor(TripleDbContext db, RoleManager<UserRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
        }

        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id);

            if (role is null)
                return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                return Failed(ApplicationStrings.RoleDeleteFailed);

            return Succeeded();
        }
    }
}
