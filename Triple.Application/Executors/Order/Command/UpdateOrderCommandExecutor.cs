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
    public class UpdateOrderCommandExecutor : Executor, IRequestHandler<UpdateOrderCommand, Result>
    {
        private TripleDbContext _db;

        public UpdateOrderCommandExecutor(TripleDbContext db)
        {
            _db = db;
        }

        public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var order = await _db.Orders.FirstOrDefaultAsync(x => x.EntityId == request.OrderId);

            if (order is null)
                return NotFound();

            var pack = await _db.Packs.FirstOrDefaultAsync(x => x.EntityId == request.PackId);

            if (pack is null)
                return NotFound();

            var price = pack.DiscountPrice * request.Quantity;

            Random random = new Random();

            var code = "";

            for (int i = 0; i < 5; i++)
            {
                code = random.Next().ToString();
            }

            order.Update(
              pack.EntityId,
              price,
              request.Quantity,
              code,
              pack.FromDate,
              pack.ToDate);

            _db.Attach(order);

            await _db.SaveChangesAsync();

            return Succeed();
        }
    }
}
