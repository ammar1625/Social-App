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
    public class ChangePassWordTokenConfigs : IEntityTypeConfiguration<ChangePassWordToken>
    {
        public void Configure(EntityTypeBuilder<ChangePassWordToken> builder)
        {
            builder.ToTable("ChangePassWordTokens");
            builder.HasKey(c => c.ChangePassWordTokenId).HasName("PK__ChangePa__223BA091CCB6FBC6");
            
            builder.Property(c => c.ChangePassWordTokenId).HasMaxLength(50);
            builder.Property(c => c.ChangePassWordToken1).HasColumnName("ChangePassWordToken");
            builder.Property(c => c.CreatedAt).HasColumnType("datetime");
            builder.Property(c => c.ExpirationDate).HasColumnType("datetime");
            builder.Property(c => c.UserId).HasMaxLength(50);

            builder.HasOne(c => c.User).WithMany(u => u.ChangePassWordTokens)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChangePas__UserI__412EB0B6");
        }
    }
}
