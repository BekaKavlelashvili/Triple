using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Aggregates.Pack;

namespace Triple.Infrastructure.Persistence.Configurations
{
    public class PackConfiguration : IEntityTypeConfiguration<Pack>
    {
        public void Configure(EntityTypeBuilder<Pack> builder)
        {
            builder.ToTable("Packs");
            builder.OwnsMany(x => x.Photos, c =>
            {
                c.ToTable("PackPhotos");
                c.HasKey(x => x.Id);
                c.HasIndex(x => x.PackId);
            });
            builder.OwnsMany(x => x.Reviews, c =>
            {
                c.ToTable("PackReviews");
                c.HasKey(x => x.Id);
                c.HasIndex(x => x.PackId);
            });
        }
    }
}
