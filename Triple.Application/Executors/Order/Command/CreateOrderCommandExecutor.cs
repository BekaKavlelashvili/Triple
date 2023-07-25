using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Commands.Order;
using Triple.Application.Shared;
using Triple.Infrastructure;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.Order.Command
{
    public class CreateOrderCommandExecutor : Executor, IRequestHandler<CreateOrderCommand, Result>
    {
        private TripleDbContext _db;
        private readonly IUserService _userService;

        public CreateOrderCommandExecutor(TripleDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            await request.CommandMustBeValidAsync();

            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            var currentUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == currentUserEmail);

            var order = new Domain.Aggregates.Order.Order(Guid.NewGuid());

            var pack = await _db.Packs.FirstOrDefaultAsync(x => x.EntityId == request.PackId);

            if (pack is null)
                return NotFound();

            pack.Quantity = pack.Quantity - request.Quantity;

            var price = pack.DiscountPrice * request.Quantity;

            Random random = new Random();

            var code = "";

            for (int i = 0; i < 5; i++)
            {
                code = random.Next().ToString();
            }

            order.Create(
                pack.EntityId,
                currentUser.OwnerId,
                price,
                request.Quantity,
                code,
                pack.FromDate,
                pack.ToDate);

            _db.Attach(order);

            await _db.SaveChangesAsync();

            return Succeeded(order.EntityId);
        }
    }
}
