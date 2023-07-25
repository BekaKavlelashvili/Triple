using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Aggregates.SupportUser;

namespace Triple.Infrastructure.Persistence.Configurations
{
    public class SupportUsersConfiguration : IEntityTypeConfiguration<SupportUser>
    {
        public void Configure(EntityTypeBuilder<SupportUser> builder)
        {
            builder.ToTable("SupportUsers");
        }
    }
}
