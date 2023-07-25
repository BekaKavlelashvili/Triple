using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Commands.Pack;
using Triple.Application.Shared;
using Triple.Infrastructure;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.Pack.Command
{
    public class AddReviewCommandExecutor : Executor, IRequestHandler<AddReviewCommand, Result>
    {
        private TripleDbContext _db;
        private readonly IUserService _userService;

        public AddReviewCommandExecutor(TripleDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public async Task<Result> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            var currentUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == currentUserEmail);

            await request.CommandMustBeValidAsync();

            var pack = await _db.Packs.FirstOrDefaultAsync(x => x.EntityId == request.PackId);

            if (pack is null)
                return NotFound();

            var order = await _db.Orders.FirstOrDefaultAsync(x => x.PackId == pack.EntityId && x.CustomerId == currentUser.OwnerId && x.Status == Domain.Aggregates.Order.Enum.OrderStatus.Confirmed);

            if (order is null)
                return NotFound();

            pack.AddReview(new Domain.Aggregates.Pack.Review
            {
                CustomerId = currentUser.OwnerId,
                Comment = request.Comment,
                Rate = request.Rate
            });

            _db.Attach(pack);

            await _db.SaveChangesAsync();

            return Succeed();
        }
    }
}
