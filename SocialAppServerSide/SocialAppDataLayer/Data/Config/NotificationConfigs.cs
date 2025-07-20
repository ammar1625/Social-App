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
    public class NotificationConfigs:IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> Builder)
        {
            Builder.ToTable("Notifications");
            Builder.HasKey(n => n.NotificationId).HasName("PK__Notifica__20CF2E12FDDA1A1D");

            Builder.Property(n => n.NotificationId).HasMaxLength(50);
            Builder.Property(n => n.Content).HasMaxLength(150);
            Builder.Property(n => n.NotificationDate).HasColumnType("datetime");
            Builder.Property(n => n.PostId).HasMaxLength(50);
            Builder.Property(n => n.UserId).HasMaxLength(50);

            Builder.HasOne(n => n.NotificationType).WithMany(nt => nt.Notifications)
                .HasForeignKey(n => n.NotificationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__Notif__6B24EA82");

            Builder.HasOne(n => n.Post).WithMany(p => p.Notifications)
                .HasForeignKey(n => n.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__PostI__693CA210");

            Builder.HasOne(n => n.User).WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Notificat__UserI__6A30C649");
        }
    }
}
