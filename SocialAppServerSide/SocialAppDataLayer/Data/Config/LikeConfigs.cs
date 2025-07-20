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
    public class LikeConfigs:IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder) 
        {
            builder.ToTable("Likes");
            builder.HasKey(l => l.LikeId).HasName("PK__Likes__A2922C14E3888649");
            
            builder.Property(l => l.LikeId).HasMaxLength(50);
            builder.Property(l => l.LikedAt).HasColumnType("datetime");
            builder.Property(l => l.PostId).HasMaxLength(50);
            builder.Property(l => l.UserId).HasMaxLength(50);
            
            builder.HasOne(l => l.Post).WithMany(p => p.Likes)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Likes__PostId__46E78A0C");

            builder.HasOne(l => l.User).WithMany(u => u.Likes)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Likes__UserId__47DBAE45");
        }
    }
}
