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
    public class ConfirmOrderCommandExecutor : Executor, IRequestHandler<ConfirmOrderCommand, Result>
    {
        private TripleDbContext _db;

        public ConfirmOrderCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var order = await _db.Orders.FirstOrDefaultAsync(x => x.EntityId == request.OrderId);

            if (order is null)
                return NotFound();

            order.Status = request.Status;

            await _db.SaveChangesAsync();

            return Succeed();
        }
    }
}
