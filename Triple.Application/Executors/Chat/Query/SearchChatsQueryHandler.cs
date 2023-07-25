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
    public class SearchChatsQueryHandler : IRequestHandler<SearchChatsQuery, QueryResultOfList<ChatDto>>
    {
        private TripleDbContext _dbContext;
        private readonly IUserService _userService;

        public SearchChatsQueryHandler(TripleDbContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        public async Task<QueryResultOfList<ChatDto>> Handle(SearchChatsQuery request, CancellationToken cancellationToken)
        {
            var currentUserEmail = UserIdentity.From(_userService.GetUser()).Email;

            var currentUser = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == currentUserEmail);

            var chats = await _dbContext.Chats.Where(c => c.Users.Select(u => u.UserID).Contains(currentUser.OwnerId)).ToListAsync();

            var result = new List<ChatDto>();

            chats.ForEach(chat =>
            {
                var lastMessage = chat.Messages.OrderByDescending(x => x.Timestamp).FirstOrDefault();

                result.Add(new ChatDto
                {
                    EntityId = chat.EntityId,
                    FirstName = chat.Users.FirstOrDefault(c => c.UserID != currentUser.OwnerId)?.Firsname,
                    LastName = chat.Users.FirstOrDefault(c => c.UserID != currentUser.OwnerId)?.Lastname,
                    LastMessageTime = lastMessage.Timestamp,
                    LastMessage = lastMessage.Text,
                    UnseenMessagesCount = chat.Messages.Where(x => x.UserId != currentUser.OwnerId && !x.Seen).Count()
                });
            });

            if (!string.IsNullOrEmpty(request.Search))
                result = result.Where(r => r.FirstName.ToUpper().Contains(request.Search.ToUpper()) ||
                r.LastName.ToUpper().Contains(request.Search.ToUpper())).ToList();

            result = result.OrderByDescending(r => r.LastMessageTime).ToList();

            return new QueryResultOfList<ChatDto>
            {
                Records = result.ToList(),
                Total = result.Count()
            };
        }
    }
}
