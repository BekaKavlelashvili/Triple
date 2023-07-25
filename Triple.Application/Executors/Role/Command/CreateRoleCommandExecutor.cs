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
    public class CreateRoleCommandExecutor : Executor, IRequestHandler<CreateRoleCommand, Result>
    {
        private readonly RoleManager<UserRole> _roleManager;
        private TripleDbContext _db;

        public CreateRoleCommandExecutor(TripleDbContext db, RoleManager<UserRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
        }

        public async Task<Result> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var roleExists = await _roleManager.FindByNameAsync(request.Name);

            if (roleExists is not null)
                return Failed(ApplicationStrings.NameMustBeUnique);

            var role = new UserRole(request.Name, request.Description);

            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded)
                return Failed(ApplicationStrings.RoleCreationFailed);

            return Succeeded(role.Id);
        }
    }
}
