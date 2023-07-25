using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Triple.Infrastructure.Identity
{
    public class UserRole : IdentityRole
    {
        public UserRole()
        {
        }

        public UserRole(string role, string description) : base(role)
        {
            Name = role;
            this.Description = description;
            this.CreateDate = DateTime.UtcNow.AddHours(4);
        }

        public string Description { get; private set; }

        public DateTime CreateDate { get; set; }

        public ICollection<ApplicationUserRole> Users { get; set; } = new HashSet<ApplicationUserRole>();

        public ICollection<RolePermissions> Permissions { get; set; } = new HashSet<RolePermissions>();

        public void SetPermissions(List<Permission> permissions)
        {
            foreach (var permission in permissions)
            {
                this.Permissions.Add(new RolePermissions
                {
                    PermissionId = permission.Id,
                    PermissionKey = permission.PermissionKey,
                    Description = permission.Description
                });
            }
        }

        public void RemovePermission(RolePermissions permission)
        {
            this.Permissions.Remove(permission);
        }

        public void ChangeDetails(string role, string description)
        {
            this.Name = role;
            this.Description = description;
        }

        public static void Configure(EntityTypeBuilder<UserRole> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("UserRole");
        }
    }
}
