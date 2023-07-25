using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Chat;
using Triple.Application.Shared;

namespace Triple.Application.Queries.Chat
{
    public class SearchChatsForAdminQuery : IRequest<QueryResultOfList<ChatForAdminDto>>
    {
        public string? Search { get; set; }
    }
}
