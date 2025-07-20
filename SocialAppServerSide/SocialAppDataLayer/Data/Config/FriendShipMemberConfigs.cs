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
    public class FriendShipMemberConfigs : IEntityTypeConfiguration<FriendShipMember>
    {
       public void Configure(EntityTypeBuilder<FriendShipMember> builder)
        {
            builder.ToTable("FriendshipMembers");
            builder.HasKey(fm => fm.FriendShipMemberId).HasName("PK__FriendSh__2E3231C1A6DF0218");
            
            builder.Property(fm => fm.FriendShipMemberId).HasMaxLength(50);
            builder.Property(fm => fm.FriendShipId).HasMaxLength(50);
            builder.Property(fm => fm.UserId).HasMaxLength(50);
            
            builder.HasOne(fm => fm.FriendShip).WithMany(f =>f.FriendShipMembers)
                .HasForeignKey(fm => fm.FriendShipId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FriendShi__Frien__75A278F5");

            builder.HasOne(fm => fm.User).WithMany(u => u.FriendShipMembers)
                .HasForeignKey(fm => fm.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FriendShi__UserI__76969D2E");
        }
    }
}
