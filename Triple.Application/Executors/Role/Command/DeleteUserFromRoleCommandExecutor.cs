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
    public class DeleteUserFromRoleCommandExecutor : Executor, IRequestHandler<DeleteUserFromRoleCommand, Result>
    {
        private TripleDbContext _db;

        public DeleteUserFromRoleCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(DeleteUserFromRoleCommand request, CancellationToken cancellationToken)
        {
            var roleUser = await _db.Set<ApplicationUserRole>().FirstOrDefaultAsync(x => x.RoleId == request.RoleId && x.UserId == request.UserId);

            if (roleUser == null)
                return NotFound();

            _db.Set<ApplicationUserRole>().Remove(roleUser);

            await _db.SaveChangesAsync();

            return Succeeded();
        }
    }
}
