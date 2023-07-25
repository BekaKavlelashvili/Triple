using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Chat
{
    public class CreateChatCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }

        public Guid? OrderId { get; set; }

        public string TemplateNote { get; set; }
    }
}
