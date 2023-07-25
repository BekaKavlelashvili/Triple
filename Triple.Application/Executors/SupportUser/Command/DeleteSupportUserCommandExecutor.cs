using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Commands.SupportUser;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.SupportUser.Command
{
    public class DeleteSupportUserCommandExecutor : Executor, IRequestHandler<DeleteSupportUserCommand, Result>
    {
        private TripleDbContext _db;

        public DeleteSupportUserCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(DeleteSupportUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _db.SupportUsers.FirstOrDefaultAsync(x => x.EntityId == request.UserId);

            if (user is null)
                return NotFound();

            _db.SupportUsers.Remove(user);

            await _db.SaveChangesAsync();

            return Succeed();
        }
    }
}
