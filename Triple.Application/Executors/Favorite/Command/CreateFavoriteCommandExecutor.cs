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
    public class CreateFavoriteCommandExecutor : Executor, IRequestHandler<CreateFavoriteCommand, Result>
    {
        private TripleDbContext _db;
        private readonly IUserService _userService;

        public CreateFavoriteCommandExecutor(TripleDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public async Task<Result> Handle(CreateFavoriteCommand request, CancellationToken cancellationToken)
        {
            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            var currentUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == currentUserEmail);

            var pack = await _db.Packs.FirstOrDefaultAsync(x => x.EntityId == request.PackId);

            if (pack is null) return NotFound();

            var favorite = new Domain.Aggregates.Favorite.Favorite(Guid.NewGuid());

            favorite.Create(currentUser.OwnerId, pack.EntityId);

            _db.Attach(favorite);

            await _db.SaveChangesAsync();

            return Succeeded(favorite.EntityId);
        }
    }
}
