using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Data.Config
{
    public class ConversationConfigs : IEntityTypeConfiguration<Conversation>
    {
        public void Configure(EntityTypeBuilder<Conversation> builder)
        {
            builder.ToTable("Conversations");
            builder.HasKey(c => c.ConversationId).HasName("PK__Conversa__C050D87756C4E455");

            builder.Property(c => c.ConversationId).HasMaxLength(50);
            builder.Property(c => c.CreatedAt).HasColumnType("datetime");
        }
    }
}
