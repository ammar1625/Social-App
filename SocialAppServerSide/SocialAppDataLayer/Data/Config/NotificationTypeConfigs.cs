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
    public class NotificationTypeConfigs:IEntityTypeConfiguration<NotificationType>
    {
        public void Configure(EntityTypeBuilder<NotificationType> Builder) 
        {
            Builder.ToTable("NotificationTypes");
            Builder.HasKey(nt => nt.NotificationTypeId).HasName("PK__Notifica__4DF4D0E20A703496");

            Builder.Property(nt => nt.NotificationTypeId).ValueGeneratedNever();
            Builder.Property(nt => nt.NotificationTypeName)
                .HasMaxLength(20)
                .HasColumnName("NotificationType");
        }
    }
}
