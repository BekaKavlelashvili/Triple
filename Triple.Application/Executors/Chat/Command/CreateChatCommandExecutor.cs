using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Commands.Chat;
using Triple.Application.Shared;
using Triple.Infrastructure;
using Triple.Infrastructure.Persistence;
using Triple.Shared;
using Triple.Shared.Results;

namespace Triple.Application.Executors.Chat.Command
{
    public class CreateChatCommandExecutor : Executor, IRequestHandler<CreateChatCommand, Result>
    {
        private TripleDbContext _db;
        private readonly IUserService _userService;

        public CreateChatCommandExecutor(TripleDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public async Task<Result> Handle(CreateChatCommand request, CancellationToken cancellationToken)
        {
            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            var currentUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == currentUserEmail);

            var users = await _db.Users.Where(x => x.OwnerId == request.UserId || x.OwnerId == currentUser.OwnerId).ToListAsync();

            var existingChat = await _db.Chats.FirstOrDefaultAsync(x => x.Users.Select(x => x.UserID).Contains(request.UserId) &&
                                                                    x.Users.Select(x => x.UserID).Contains(currentUser.OwnerId));

            if (existingChat is not null)
                return Succeeded(existingChat.EntityId);

            var chat = new Domain.Aggregates.Chat.Chat(Guid.NewGuid());

            chat.TemplateNote = request.TemplateNote;
            chat.OrderId = request.OrderId;

            users.ForEach(x =>
            {
                chat.AddUsers(new Domain.Aggregates.Chat.ChatUsers()
                {
                    UserID = x.OwnerId,
                    Firsname = x.FirstName,
                    Lastname = x.LastName,
                });
            });

            _db.Attach(chat);

            await _db.SaveChangesAsync();

            return Succeeded(chat.EntityId);
        }
    }
}
