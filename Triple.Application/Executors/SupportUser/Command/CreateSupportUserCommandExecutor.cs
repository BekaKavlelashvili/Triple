using MediatR;
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
    public class CreateSupportUserCommandExecutor : Executor, IRequestHandler<CreateSupportUserCommand, Result>
    {
        private TripleDbContext _db;

        public CreateSupportUserCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(CreateSupportUserCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var user = new Domain.Aggregates.SupportUser.SupportUser(Guid.NewGuid());

            user.CreateOrUpdate(request.FirstName, request.LastName, request.Email);

            _db.Attach(user);

            await _db.SaveChangesAsync();

            return Succeeded(user.EntityId);
        }
    }
}
