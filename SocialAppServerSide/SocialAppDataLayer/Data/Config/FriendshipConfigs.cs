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
    public class FriendshipConfigs : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.ToTable("Friendships");
            builder.HasKey(f => f.FriendShipId).HasName("PK__Friendsh__190D6358E1593F90");

            builder.Property(f => f.FriendShipId).HasMaxLength(50);
            builder.Property(f => f.CreatedAt).HasColumnType("datetime");
        }
    }
}
