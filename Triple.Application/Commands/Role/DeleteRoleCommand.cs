using Triple.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Commands.Role
{
    public class DeleteRoleCommand : IRequest<Result>
    {
        public DeleteRoleCommand(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}