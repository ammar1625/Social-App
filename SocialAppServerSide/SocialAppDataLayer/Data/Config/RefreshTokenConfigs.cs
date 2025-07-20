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
    public class RefreshTokenConfigs:IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refreshtokens");
            builder.HasKey(r => r.RefreshTokenId).HasName("PK__RefreshT__F5845E39FE296746");

            builder.Property(r => r.RefreshTokenId).HasMaxLength(50);
            builder.Property(r => r.CreatedAt).HasColumnType("datetime");
            builder.Property(r => r.ExpirationDate).HasColumnType("datetime");
            builder.Property(r => r.RefreshToken1)
                .HasMaxLength(100)
                .HasColumnName("RefreshToken");
            builder.Property(r => r.UserId).HasMaxLength(50);

            builder.HasOne(r => r.User).WithMany(u => u.RefreshTokens)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RefreshTo__UserI__3E52440B");
        }
    }
}
