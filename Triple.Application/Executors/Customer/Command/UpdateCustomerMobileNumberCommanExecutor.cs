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
    public class UpdateCustomerMobileNumberCommanExecutor : Executor, IRequestHandler<UpdateCustomerMobileNumberCommand, Result>
    {
        private TripleDbContext _db;

        public UpdateCustomerMobileNumberCommanExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(UpdateCustomerMobileNumberCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var customer = await _db.Customers.FirstOrDefaultAsync(x => x.EntityId == request.EntityId);

            if (customer is null)
                return NotFound();

            customer.MobileNumber = request.MobileNumber;

            _db.Attach(customer);

            await _db.SaveChangesAsync();

            return Succeeded();
        }
    }
}
