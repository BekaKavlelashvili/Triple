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
    public class UpdateSupportUserCommandExecutor : Executor, IRequestHandler<UpdateSupportUserCommand, Result>
    {
        private TripleDbContext _db;

        public UpdateSupportUserCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(UpdateSupportUserCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var user = await _db.SupportUsers.FirstOrDefaultAsync(x => x.EntityId == request.UserId);

            if (user is null)
                return NotFound();

            user.CreateOrUpdate(request.FirstName, request.LastName, request.Email);

            _db.SupportUsers.Update(user);

            await _db.SaveChangesAsync();

            return Succeeded(user.EntityId);
        }
    }
}
