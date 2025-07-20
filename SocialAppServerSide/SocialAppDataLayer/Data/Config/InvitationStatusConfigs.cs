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
    public class InvitationStatusConfigs:IEntityTypeConfiguration<InvitationStatus>
    {
        public void Configure(EntityTypeBuilder<InvitationStatus> builder)
        {
            builder.ToTable("InvitationStatuses");
            builder.HasKey(e => e.StatusId).HasName("PK__Invitati__C8EE206359160507");

            builder.Property(e => e.StatusId).ValueGeneratedNever();
            builder.Property(e => e.Status).HasMaxLength(30);
        }

       
    }
}
