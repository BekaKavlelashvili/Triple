using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Commands.Favorite;
using Triple.Application.Shared;
using Triple.Infrastructure;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.Favorite.Command
{
    public class DeleteFavoriteCommandExecutor : Executor, IRequestHandler<DeleteFavoriteCommand, Result>
    {
        private TripleDbContext _db;
        private readonly IUserService _userService;

        public DeleteFavoriteCommandExecutor(TripleDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public async Task<Result> Handle(DeleteFavoriteCommand request, CancellationToken cancellationToken)
        {
            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            var currentUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == currentUserEmail);

            var favorite = await _db.Favorites.FirstOrDefaultAsync(x => x.EntityId == request.FavoriteId && x.CustomerId == currentUser.OwnerId);

            if (favorite is null) return NotFound();

            _db.Favorites.Remove(favorite);

            await _db.SaveChangesAsync();

            return Succeed();
        }
    }
}
