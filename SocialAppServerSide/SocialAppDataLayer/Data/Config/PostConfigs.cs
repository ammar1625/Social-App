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
    public class PostConfigs:IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(p => p.PostId).HasName("PK__Posts__AA1260186BEAE314");

            builder.Property(p => p.PostId).HasMaxLength(50);
            builder.Property(p => p.CreatedAt).HasColumnType("datetime");
            builder.Property(p => p.MediaUrl).HasMaxLength(255);
            builder.Property(p => p.UserId).HasMaxLength(50);

            builder.HasOne(p => p.User).WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Posts__UserId__440B1D61");
        }
    }
}
