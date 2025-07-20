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
    public class ConversationMemberConfigs : IEntityTypeConfiguration<ConversationMember>
    {
        public void Configure(EntityTypeBuilder<ConversationMember> builder)
        {
            builder.ToTable("ConversationMembers");
            builder.HasKey(cm => cm.ConversationMemberId).HasName("PK__Conversa__6CF984272C762FF9");
            
            builder.Property(cm => cm.ConversationMemberId).HasMaxLength(50);
            builder.Property(cm => cm.ConversationId).HasMaxLength(50);
            builder.Property(cm => cm.UserId).HasMaxLength(50);
            
            builder.HasOne(cm => cm.Conversation).WithMany(c => c.ConversationMembers)
                .HasForeignKey(cm => cm.ConversationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Conversat__Conve__6FE99F9F");

            builder.HasOne(cm => cm.User).WithMany(u => u.ConversationMembers)
                .HasForeignKey(cm => cm.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Conversat__UserI__70DDC3D8");
        }
    }
}
