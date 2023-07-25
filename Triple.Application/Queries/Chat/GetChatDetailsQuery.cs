using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Chat;

namespace Triple.Application.Queries.Chat
{
    public class GetChatDetailsQuery : IRequest<ChatDetailsDto?>
    {
        public Guid ChatId { get; set; }
    }
}
