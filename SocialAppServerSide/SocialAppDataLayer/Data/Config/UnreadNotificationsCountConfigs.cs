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
    public class UnreadNotificationsCountConfigs : IEntityTypeConfiguration<UnreadNotificationsCount>
    {
        public void Configure(EntityTypeBuilder<UnreadNotificationsCount>builder)
        {
            builder.ToTable("SP_GetCountOfUnreadNotificationsByUserId").HasNoKey();
        }
    }
}
