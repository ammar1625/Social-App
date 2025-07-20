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
    public class MessageConfigs:IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder) 
        {
            builder.ToTable("Messages");
            builder.HasKey(m => m.MessageId).HasName("PK__Messages__C87C0C9C03848B6D");
            
            builder.Property(m => m.MessageId).HasMaxLength(50);
            builder.Property(m => m.ConversationId).HasMaxLength(50);
            builder.Property(m => m.MessageMediaUrl).HasMaxLength(255);
            builder.Property(m => m.SenderId).HasMaxLength(50);
            builder.Property(m => m.SentAt).HasColumnType("datetime");
            
            builder.HasOne(m => m.Conversation).WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Conver__7A672E12");

            builder.HasOne(m => m.Sender).WithMany(u => u.Messages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Messages__Sender__797309D9");
        }
    }
}
