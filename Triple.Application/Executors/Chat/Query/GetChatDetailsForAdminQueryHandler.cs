using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Chat;
using Triple.Application.Queries.Chat;
using Triple.Infrastructure.Identity;
using Triple.Infrastructure.Persistence;

namespace Triple.Application.Executors.Chat.Query
{
    public class GetChatDetailsForAdminQueryHandler : IRequestHandler<GetChatDetailsForAdminQuery, ChatDetailsForAdminDto?>
    {
        private TripleDbContext _dbContext;

        public GetChatDetailsForAdminQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ChatDetailsForAdminDto?> Handle(GetChatDetailsForAdminQuery request, CancellationToken cancellationToken)
        {
            return await (from chat in _dbContext.Chats.Where(x => x.EntityId == request.ChatId)
                          let messages = from message in chat.Messages
                                         select new MessageForAdminDto
                                         {
                                             Firstname = message.Firstname,
                                             Lastname = message.Lastname,
                                             Text = message.Text,
                                             Timestamp = message.Timestamp,
                                         }
                          select new ChatDetailsForAdminDto
                          {
                              ChatId = chat.EntityId,
                              Messages = messages.OrderBy(x=> x.Timestamp).ToList(),
                          }).FirstOrDefaultAsync();
        }
    }
}
