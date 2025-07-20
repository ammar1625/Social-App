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
    public class UserConfigs:IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.UserId).HasName("PK__Users__1788CC4C716E9855");

            builder.ToTable(tb => tb.HasTrigger("InsteadOfDeleteUsers"));

            builder.Property(u => u.UserId).HasMaxLength(50);
            builder.Property(u => u.Email).HasMaxLength(50);
            builder.Property(u => u.FirstName).HasMaxLength(50);
            builder.Property(u => u.LastName).HasMaxLength(50);
            builder.Property(u => u.PassWord).HasMaxLength(150);
            builder.Property(u => u.Phone).HasMaxLength(20);
            builder.Property(u => u.ProfilePic).HasMaxLength(255);
            builder.Property(u => u.UserName).HasMaxLength(50);
        }
    }
}
