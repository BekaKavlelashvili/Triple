using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Commands.Chat;
using Triple.Application.Dtos.Chat;
using Triple.Application.Shared;
using Triple.Infrastructure;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.Chat.Command
{
    public class MarkAsSeenCommandExecutor : Executor, IRequestHandler<MarkAsSeenCommand, Result>
    {
        private TripleDbContext _db;
        private readonly IUserService _userService;

        public MarkAsSeenCommandExecutor(TripleDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public async Task<Result> Handle(MarkAsSeenCommand request, CancellationToken cancellationToken)
        {
            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            var currentUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == currentUserEmail);

            var chat = await _db.Chats.FirstOrDefaultAsync(x => x.EntityId == request.ChatId);

            if (chat is null)
                return NotFound();

            chat.LastOpenTime = DateTime.UtcNow;

            var messages = chat.Messages.Where(x=> x.UserId != currentUser.OwnerId && !x.Seen).ToList();

            messages.ForEach(message =>
            {
                message.Seen = true;
            });

            _db.Attach(chat);

            await _db.SaveChangesAsync();

            return Succeeded(chat.EntityId);
        }
    }
}
