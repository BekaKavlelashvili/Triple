using System;
using System.Collections.Generic;
using System.Text;

namespace Triple.Infrastructure.Identity
{
    public class Permission
    {
        public int Id { get; set; }

        public int PermissionGroupId { get; set; }

        public string PermissionKey { get; set; }

        public string Description { get; set; }

        public PermissionGroup Group { get; set; }
    }
}
