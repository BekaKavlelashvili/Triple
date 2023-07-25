using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Commands.Customer;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.Customer.Command
{
    public class CreateCustomerCommandExecutor : Executor, IRequestHandler<CreateCustomerCommand, Result>
    {
        private TripleDbContext _db;

        public CreateCustomerCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var customer = new Domain.Aggregates.Customer.Customer(Guid.NewGuid());

            customer.Create(request.FirstName, request.LastName, request.Email, request.MobileNumber, "Georgia");

            _db.Attach(customer);

            await _db.SaveChangesAsync();

            return Succeeded(customer.EntityId);
        }
    }
}
