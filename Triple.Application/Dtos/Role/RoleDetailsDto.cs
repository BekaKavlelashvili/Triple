using Triple.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Dtos.Role
{
    public class RoleDetailsDto
    {
        public RoleDto Role { get; set; }

        public List<PermissionDto> Permissions { get; set; }

        public List<UserDto> Users { get; set; }
    }
}
