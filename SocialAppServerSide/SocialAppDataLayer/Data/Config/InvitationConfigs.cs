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
    public class InvitationConfigs : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.ToTable("Invitations");
            builder.HasKey(i => i.InvitationId).HasName("PK__Invitati__033C8DCF47A76372");
            
            builder.Property(i => i.InvitationId).HasMaxLength(50);
            builder.Property(i => i.InvitationStatus).HasColumnName("invitationStatus");
            builder.Property(i => i.RecieverId).HasMaxLength(50);
            builder.Property(i => i.SenderId).HasMaxLength(50);
            builder.Property(i => i.SentAt).HasColumnType("datetime");
            
            builder.HasOne(i => i.InvitationStatusNavigation).WithMany(Is => Is.Invitations)
                .HasForeignKey(i => i.InvitationStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invitatio__invit__4E88ABD4");

            builder.HasOne(i => i.Reciever).WithMany(u => u.RecievedInvitations)
                .HasForeignKey(i => i.RecieverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invitatio__Recie__4D94879B");

            builder.HasOne(i => i.Sender).WithMany(u => u.SentInvitations)
                .HasForeignKey(i => i.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invitatio__Sende__4CA06362");
        }
    }
}
