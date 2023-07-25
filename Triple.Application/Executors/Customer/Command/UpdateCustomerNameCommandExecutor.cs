using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class UpdateCustomerNameCommandExecutor : Executor, IRequestHandler<UpdateCustomerNameCommand, Result>
    {
        private TripleDbContext _db;

        public UpdateCustomerNameCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(UpdateCustomerNameCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var customer = await _db.Customers.FirstOrDefaultAsync(x => x.EntityId == request.EntityId);

            if (customer is null)
                return NotFound();

            customer.FirstName = request.FirstName;
            customer.LastName = request.LastName;

            _db.Attach(customer);

            await _db.SaveChangesAsync();

            return Succeeded();
        }
    }
}
