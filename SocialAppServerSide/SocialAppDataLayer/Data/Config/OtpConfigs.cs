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
    public class OtpConfigs:IEntityTypeConfiguration<Otp>
    {
        public void Configure(EntityTypeBuilder<Otp> Builder)
        {
            Builder.ToTable("Otps");
            Builder.HasKey(e => e.Id).HasName("PK__Otps__3214EC07DA9E318A");

            Builder.Property(o => o.Id).HasMaxLength(50);
            Builder.Property(o => o.CreatedAt).HasColumnType("datetime");
            Builder.Property(o => o.ExpirationDate).HasColumnType("datetime");
            Builder.Property(o => o.UserId).HasMaxLength(50);

            Builder.HasOne(o => o.User).WithMany(u => u.Otps)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Otps__UserId__3B75D760");
        }
    }
}
