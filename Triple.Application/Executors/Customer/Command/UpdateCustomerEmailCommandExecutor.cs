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
    public class UpdateCustomerEmailCommandExecutor : Executor, IRequestHandler<UpdateCustomerEmailCommand, Result>
    {
        private TripleDbContext _db;

        public UpdateCustomerEmailCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(UpdateCustomerEmailCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var customer = await _db.Customers.FirstOrDefaultAsync(x => x.EntityId == request.EntityId);

            if (customer is null)
                return NotFound();

            customer.Email = request.Email;

            _db.Attach(customer);

            await _db.SaveChangesAsync();

            return Succeeded();
        }
    }
}
