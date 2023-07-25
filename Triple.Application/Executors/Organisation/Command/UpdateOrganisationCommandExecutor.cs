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
    public class UpdateOrganisationCommandExecutor : Executor, IRequestHandler<UpdateOrganisationCommand, Result>
    {
        private TripleDbContext _db;

        public UpdateOrganisationCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(UpdateOrganisationCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var organisation = await _db.Organisations.FirstOrDefaultAsync(x => x.EntityId == request.EntityId);

            if (organisation is null)
                return NotFound();

            organisation.CreateOrUpdate(
                request.Name,
                request.Email,
                request.Network,
                request.OrganisationAddress,
                request.Commission,
                request.OperatorFirstName,
                request.OperatorLastName,
                request.OperatorPhone,
                request.Category);

            _db.Attach(organisation);

            await _db.SaveChangesAsync();

            return Succeeded(organisation.EntityId);
        }
    }
}
