using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Chat;
using Triple.Application.Queries.Chat;
using Triple.Application.Shared;
using Triple.Infrastructure;
using Triple.Infrastructure.Persistence;

namespace Triple.Application.Executors.Chat.Query
{
    public class GetChatDetailsQueryHandler : IRequestHandler<GetChatDetailsQuery, ChatDetailsDto?>
    {
        private TripleDbContext _dbContext;
        private readonly IUserService _userService;

        public GetChatDetailsQueryHandler(TripleDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<ChatDetailsDto?> Handle(GetChatDetailsQuery request, CancellationToken cancellationToken)
        {
            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            var currentUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == currentUserEmail);

            return await (from chat in _dbContext.Chats
                          where chat.EntityId == request.ChatId
                          let messages = from message in chat.Messages
                                         select new MessageDto
                                         {
                                             UserId = message.UserId,
                                             Firstname = message.Firstname,
                                             Lastname = message.Lastname,
                                             Text = message.Text,
                                             Timestamp = message.Timestamp,
                                             IsCurrentUser = message.UserId == currentUser.OwnerId,
                                             Seen = message.Seen
                                         }
                          select new ChatDetailsDto
                          {
                              ChatId = chat.EntityId,
                              Messages = messages.OrderBy(m => m.Timestamp).ToList()
                          }).FirstOrDefaultAsync();
        }
    }
}
