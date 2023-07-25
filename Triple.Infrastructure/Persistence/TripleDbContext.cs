using Triple.Infrastructure;
using Triple.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Infrastructure;
using Triple.Infrastructure.Persistence.Configuartions;
using Triple.Domain.Aggregates.Organisation;
using Triple.Infrastructure.Persistence.Configurations;
using Triple.Domain.Aggregates.Pack;
using Triple.Domain.Aggregates.Customer;
using Triple.Domain.Aggregates.Order;
using Triple.Domain.Aggregates.Coordinate;
using Triple.Domain.Aggregates.Favorite;
using Triple.Domain.Aggregates.SupportUser;
using Triple.Domain.Aggregates.Chat;

namespace Triple.Infrastructure.Persistence
{
    public class TripleDbContext : IdentityDbContext<ApplicationUser, UserRole, string>
    {
        public TripleDbContext(DbContextOptions<TripleDbContext> options) : base(options)
        {
        }

        public override DbSet<ApplicationUser> Users { get; set; }

        public override DbSet<UserRole> Roles { get; set; }

        public DbSet<PermissionGroup> PermissionGroups { get; set; }

        public DbSet<Organisation> Organisations { get; set; }

        public DbSet<Pack> Packs { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Coordinate> Coordinates { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<SupportUser> SupportUsers { get; set; }

        public DbSet<Chat> Chats { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ApplicationUser.Configure(builder.Entity<ApplicationUser>());
            UserRole.Configure(builder.Entity<UserRole>());
            builder.ApplyConfiguration(new PermissionGroupConfiguration());
            builder.ApplyConfiguration(new OrganisationConfiguration());
            builder.ApplyConfiguration(new PackConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new CoordinateConfiguration());
            builder.ApplyConfiguration(new FavoriteConfiguration());
            builder.ApplyConfiguration(new SupportUsersConfiguration());
            builder.ApplyConfiguration(new ChatConfiguration());
        }
    }
}
