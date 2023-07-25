using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Shared.Results;

namespace Triple.Application.Commands.Order
{
    public class DeleteOrderCommand : IRequest<Result>
    {
        public Guid EntityId { get; set; }

        public DeleteOrderCommand(Guid entityId)
        {
            EntityId = entityId;
        }
    }
}
