using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Commands.Organisation;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.Organisation.Command
{
    public class DeleteOrganisationCommandExecutor : Executor, IRequestHandler<DeleteOrganisationCommand, Result>
    {
        private TripleDbContext _db;

        public DeleteOrganisationCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(DeleteOrganisationCommand request, CancellationToken cancellationToken)
        {
            var organisation = await _db.Organisations.FirstOrDefaultAsync(x => x.EntityId == request.EntityId);

            if (organisation is null)
                return NotFound();

            _db.Organisations.Remove(organisation);

            await _db.SaveChangesAsync();

            return Succeed();
        }
    }
}
