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
    public class UpdateCustomerCommandExecutor : Executor, IRequestHandler<UpdateCustomerCommand, Result>
    {
        private TripleDbContext _db;

        public UpdateCustomerCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var customer = await _db.Customers.FirstOrDefaultAsync(x=> x.EntityId == request.EntityId);

            if (customer is null)
                return NotFound();

            customer.Update(request.FirstName, request.LastName, request.Email, request.MobileNumber);

            _db.Update(customer);

            await _db.SaveChangesAsync();

            return Succeeded(customer.EntityId);
        }
    }
}
