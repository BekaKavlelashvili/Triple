using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Commands.Order;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.Order.Command
{
    public class DeleteOrderCommandExecutor : Executor, IRequestHandler<DeleteOrderCommand, Result>
    {
        private TripleDbContext _db;

        public DeleteOrderCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _db.Orders.FirstOrDefaultAsync(x => x.EntityId == request.EntityId);

            if (order is null)
                return NotFound();

            _db.Orders.Remove(order);

            await _db.SaveChangesAsync();

            return Succeed();
        }
    }
}
