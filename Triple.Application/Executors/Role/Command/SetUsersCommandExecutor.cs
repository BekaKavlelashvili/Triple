using Triple.Application.Commands.Role;
using Triple.Infrastructure.Identity;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
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
    public class SetUsersCommandExecutor : Executor, IRequestHandler<SetUsersCommand, Result>
    {
        private readonly RoleManager<UserRole> _roleManager;
        private TripleDbContext _db;

        public SetUsersCommandExecutor(TripleDbContext db, RoleManager<UserRole> roleManager)
        {
            _db = db;
            _roleManager = roleManager;
        }

        public async Task<Result> Handle(SetUsersCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId);

            if (role is null)
                return NotFound();

            var users = await _db.Users.Where(u => request.UserIds.Contains(u.Id)).ToListAsync();

            users.ForEach(x =>
            {
                x.AddRole(role);
                _db.Attach(x);
            });

            await _db.SaveChangesAsync();

            return Succeeded();
        }
    }
}
