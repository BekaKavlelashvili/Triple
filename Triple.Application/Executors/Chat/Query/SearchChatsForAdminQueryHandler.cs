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
using Triple.Domain.Aggregates.SupportUser;
using Triple.Infrastructure.Identity;
using Triple.Infrastructure.Persistence;

namespace Triple.Application.Executors.Chat.Query
{
    public class SearchChatsForAdminQueryHandler : IRequestHandler<SearchChatsForAdminQuery, QueryResultOfList<ChatForAdminDto>>
    {
        private TripleDbContext _dbContext;

        public SearchChatsForAdminQueryHandler(TripleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<QueryResultOfList<ChatForAdminDto>> Handle(SearchChatsForAdminQuery request, CancellationToken cancellationToken)
        {
            var chats = await _dbContext.Chats.ToListAsync();

            var chatUsers = new List<ApplicationUser>();

            chats.ForEach(chat =>
            {
                chat.Users.ToList().ForEach(user =>
                {
                    chatUsers.Add(new ApplicationUser
                    {
                        FirstName = user.Firsname,
                        LastName = user.Lastname,
                        OwnerId = user.UserID
                    });
                });
            });

            var supportUser = new Domain.Aggregates.SupportUser.SupportUser();
            var customer = new Domain.Aggregates.Customer.Customer();

            var supportUsers = await _dbContext.SupportUsers.ToListAsync();

            chatUsers.ForEach(user =>
            {

                if (supportUsers.Select(x => x.EntityId).Contains(user.OwnerId))
                {
                    supportUser = new Domain.Aggregates.SupportUser.SupportUser
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EntityId = user.OwnerId
                    };
                }
                else
                {
                    customer = new Domain.Aggregates.Customer.Customer
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EntityId = user.OwnerId
                    };
                }
            });

            var result = new List<ChatForAdminDto>();

            chats.ForEach(chat =>
            {
                var lastMessage = chat.Messages.OrderByDescending(x => x.Timestamp).FirstOrDefault();

                result.Add(new ChatForAdminDto
                {
                    EntityId = chat.EntityId,
                    CustomerFirstName = customer.FirstName,
                    CustomerLastName = customer.LastName,
                    SupportFirstName = supportUser.FirstName,
                    SupportLastName = supportUser.LastName,
                    LastMessageTime = lastMessage.Timestamp,
                    LastMessage = lastMessage.Text,
                });
            });

            if (!string.IsNullOrEmpty(request.Search))
                result = result.Where(r => r.CustomerFirstName.ToUpper().Contains(request.Search.ToUpper()) ||
                r.CustomerLastName.ToUpper().Contains(request.Search.ToUpper()) || r.SupportFirstName.ToUpper().Contains(request.Search.ToUpper()) ||
                r.SupportLastName.ToUpper().Contains(request.Search.ToUpper())).ToList();

            result = result.OrderByDescending(r => r.LastMessageTime).ToList();

            return new QueryResultOfList<ChatForAdminDto>
            {
                Records = result.ToList(),
                Total = result.Count()
            };
        }
    }
}
