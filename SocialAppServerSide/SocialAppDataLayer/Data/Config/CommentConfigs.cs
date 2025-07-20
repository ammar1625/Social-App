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
    public class CommentConfigs : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(c => c.CommentId).HasName("PK__Comments__C3B4DFCAF07A37FE");
           
            builder.Property(c => c.CommentId).HasMaxLength(50);
            builder.Property(c => c.PostId).HasMaxLength(50);
            builder.Property(c => c.SentAt).HasColumnType("datetime");
            builder.Property(c => c.UserId).HasMaxLength(50);
           
            builder.HasOne(c => c.Post).WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__PostId__43D61337");

            builder.HasOne(c => c.User).WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comments__UserId__42E1EEFE");
        }
    }
}
