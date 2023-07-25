using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Pack
{
    public class DeletePackCommand : IRequest<Result>
    {
        public Guid EntityId { get; set; }

        public DeletePackCommand(Guid entityId)
        {
            EntityId = entityId;
        }
    }
}
