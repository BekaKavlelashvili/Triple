using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Application.Dtos.Chat;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Chat
{
    public class CreateMessageCommand : IRequest<MessageDto>
    {
        public Guid ChatId { get; set; }

        public string Message { get; set; }
    }
}
