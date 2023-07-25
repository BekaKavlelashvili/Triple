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
    public class CreateMessageCommandExecutor : Executor, IRequestHandler<CreateMessageCommand, MessageDto>
    {
        private TripleDbContext _db;
        private readonly IUserService _userService;

        public CreateMessageCommandExecutor(TripleDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public async Task<MessageDto> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            var currentUser = await _db.Users.FirstOrDefaultAsync(x => x.Email == currentUserEmail);

            var chat = await _db.Chats.FirstOrDefaultAsync(x => x.EntityId == request.ChatId);

            var message = new Domain.Aggregates.Chat.Message
            {
                ChatId = chat.Id,
                Firstname = chat.Users.FirstOrDefault(x => x.UserID == currentUser.OwnerId).Firsname,
                Lastname = chat.Users.FirstOrDefault(x => x.UserID == currentUser.OwnerId).Lastname,
                Text = request.Message,
                Timestamp = DateTime.Now
            };

            chat.AddMessage(message);

            _db.Attach(chat);

            await _db.SaveChangesAsync();

            return new MessageDto()
            {
                UserId = message.UserId,
                Firstname = message.Firstname,
                Lastname = message.Lastname,
                Text = message.Text,
                Timestamp = message.Timestamp
            };
        }
    }
}
