using Triple.Application.Dtos.Role;
using Triple.Shared.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Commands.Role
{
    public class SetPermissionCommand : IRequest<Result>
    {
        public string RoleId { get; set; }

        public List<PermissionDto> Permissions { get; set; }
    }
}
