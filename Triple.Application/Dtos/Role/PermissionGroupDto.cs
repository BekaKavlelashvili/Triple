using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Application.Dtos.Role
{
    public class PermissionGroupDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<PermissionDto> Permissions { get; set; }
    }
}
