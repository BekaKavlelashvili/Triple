using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Triple.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            CreateDate = DateTime.UtcNow.AddHours(4);
        }

        public Guid OwnerId { get; set; }

        public DateTime CreateDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<ApplicationUserRole> Roles { get; set; } = new HashSet<ApplicationUserRole>();

        public EntityState EntityState { get; set; }

        public void Delete() => this.EntityState = EntityState.Deleted;

        public void SetRoles(List<UserRole> roles)
        {
            roles.ForEach(x =>
            {
                this.Roles.Add(new ApplicationUserRole
                {
                    RoleId = x.Id,
                    UserId = this.Id
                });
            });
        }

        public void AddRole(UserRole role)
        {
            this.Roles.Add(new ApplicationUserRole
            {
                RoleId = role.Id,
                UserId = this.Id
            });
        }

        public static void Configure(EntityTypeBuilder<ApplicationUser> entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("ApplicationUser");
        }
    }
}
