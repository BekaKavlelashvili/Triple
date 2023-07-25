using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Chat
{
    public class MarkAsSeenCommand : IRequest<Result>
    {
        public Guid ChatId { get; set; }
    }
}
