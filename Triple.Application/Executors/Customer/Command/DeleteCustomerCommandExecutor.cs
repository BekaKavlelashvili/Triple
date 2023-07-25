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
    public class DeleteCustomerCommandExecutor : Executor, IRequestHandler<DeleteCustomerCommand, Result>
    {
        private TripleDbContext _db;

        public DeleteCustomerCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _db.Customers.FirstOrDefaultAsync(x => x.EntityId == request.EntityId);

            if (customer is null)
                return NotFound();

            _db.Customers.Remove(customer);

            await _db.SaveChangesAsync();

            return Succeeded();
        }
    }
}
