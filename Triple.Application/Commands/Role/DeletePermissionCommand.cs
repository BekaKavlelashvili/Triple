using Triple.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Commands.Role
{
    public class DeletePermissionCommand : IRequest<Result>
    {
        public string RoleId { get; set; }

        public int PermissionId { get; set; }
    }
}
