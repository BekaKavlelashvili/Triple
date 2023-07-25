using Triple.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triple.Infrastructure.Persistence.Configuartions
{
    public class PermissionGroupConfiguration : IEntityTypeConfiguration<PermissionGroup>
    {
        public void Configure(EntityTypeBuilder<PermissionGroup> builder)
        {
            builder.ToTable("PermissionGroup");
            builder.OwnsMany(x => x.Permissions, c =>
            {
                c.ToTable("Permissions");
                c.HasKey(x => x.Id);
                c.HasIndex(x => x.PermissionGroupId);
            });
        }
    }
}
