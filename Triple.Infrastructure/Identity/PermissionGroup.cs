﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Infrastructure.Identity
{
    public class PermissionGroup
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public ICollection<Permission> Permissions { get; set; } = new HashSet<Permission>();
    }
}
