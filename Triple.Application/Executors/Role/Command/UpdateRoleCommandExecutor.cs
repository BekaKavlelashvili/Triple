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
    public class UpdateRoleCommandExecutor : Executor, IRequestHandler<UpdateRoleCommand, Result>
    {
        private readonly RoleManager<UserRole> _roleManager;
        private TripleDbContext _db;

        public UpdateRoleCommandExecutor(TripleDbContext db, RoleManager<UserRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
        }

        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var role = await _roleManager.FindByIdAsync(request.Id);

            if (role is null)
                return NotFound();

            role.ChangeDetails(request.Name, request.Description);

            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded)
                return Failed(ApplicationStrings.RoleUpdateFailed);

            return Succeeded(role.Id);
        }
    }
}
