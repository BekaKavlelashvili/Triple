using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Shared.Results;

namespace Triple.Application.Commands.SupportUser
{
    public class DeleteSupportUserCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }

        public DeleteSupportUserCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
