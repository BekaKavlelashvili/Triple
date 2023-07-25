using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Shared;
using Triple.Infrastructure.Identity;
using Triple.Infrastructure.Persistence;

namespace Triple.Infrastructure.Persistence
{
    public static class Seed
    {
        public static void Initialize(TripleDbContext db, IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<UserRole> roleManager)
        {
            InitialPermissions(db, configuration);

            var roles = db.Set<ApplicationUserRole>().ToList();

            if (roles == null || !roles.Any())
            {
                var permissions = db.PermissionGroups.AsNoTracking().SelectMany(p => p.Permissions).ToList();

                var role = new UserRole("Admin", "Administrator's Role");

                roleManager.CreateAsync(role);

                role.SetPermissions(permissions);

                db.Add(role);

                db.SaveChanges();
            }
            else
            {
                UpdateAdminPermissionsIfNecessary(db);
            }

            var users = db.Set<ApplicationUser>().ToList();

            if (users == null || !users.Any())
            {
                var role = roleManager.FindByNameAsync("Admin").Result;

                var user = new ApplicationUser()
                {
                    FirstName = "admin",
                    LastName = "admin",
                    UserName = "Administrator",
                    Email = "admin@gmail.com",
                };

                userManager.CreateAsync(user, "admin1234");

                db.Add(user);

                user.Roles.Add(new ApplicationUserRole
                {
                    RoleId = role.Id,
                    UserId = user.Id
                });

                db.SaveChanges();
            }
        }

        public static void UpdateAdminPermissionsIfNecessary(TripleDbContext db)
        {
            var adminRole = db.Roles.Include(x => x.Permissions).FirstOrDefault(x => x.Name == "Admin");
            var permissions = db.PermissionGroups.AsNoTracking().SelectMany(p => p.Permissions).ToList();
            var newPermissions = new List<Permission>();
            foreach (var permission in permissions)
            {
                if (!adminRole.Permissions.Select(p => p.PermissionKey).Any(p => p == permission.PermissionKey))
                {
                    newPermissions.Add(permission);
                }
            }

            adminRole.SetPermissions(newPermissions);

            db.SaveChanges();
        }

        public static void InitialPermissions(TripleDbContext db, IConfiguration configuration)
        {
            var configSection = configuration.GetSection("PermissionConfig");
            foreach (var section in configSection.GetChildren())
            {
                var permissionGroup = db.PermissionGroups.FirstOrDefault(g => g.GroupName == section.Key);

                if (permissionGroup is null)
                    permissionGroup = new PermissionGroup() { GroupName = section.Key };

                foreach (var sectionItem in section.GetChildren()
                                                   .Select(x => new { Key = x.GetSection("Key").Value, Description = x.GetSection("Description").Value })
                                                   .ToList())
                {
                    if (!permissionGroup.Permissions.Select(p => p.PermissionKey).Contains(sectionItem.Key))
                    {
                        permissionGroup.Permissions.Add(new Permission()
                        {
                            PermissionKey = sectionItem.Key,
                            Description = sectionItem.Description
                        });
                    }
                }

                db.Update(permissionGroup);
            }

            db.SaveChanges();
        }
    }
}

