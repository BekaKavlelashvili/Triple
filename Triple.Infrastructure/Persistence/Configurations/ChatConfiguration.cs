using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triple.Domain.Aggregates.Chat;

namespace Triple.Infrastructure.Persistence.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chats");
            builder.OwnsMany(x => x.Users, c =>
            {
                c.ToTable("ChatUsers");
                c.HasKey(x => x.Id);
                c.HasIndex(x => x.ChatId);
            });
            builder.OwnsMany(x => x.Messages, c =>
            {
                c.ToTable("ChatMessages");
                c.HasKey(x => x.Id);
                c.HasIndex(x => x.ChatId);
            });
        }
    }
}
