using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Infrastructure.Identity
{
    public class RolePermissions
    {
        public int Id { get; set; }

        public UserRole Role { get; set; }

        public string RoleId { get; set; }

        public int PermissionId { get; set; }

        public string PermissionKey { get; set; }

        public string Description { get; set; }
    }
}
