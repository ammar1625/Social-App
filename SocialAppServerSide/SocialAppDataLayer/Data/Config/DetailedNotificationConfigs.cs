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
    internal class DetailedNotificationConfigs : IEntityTypeConfiguration<DetailedNotification>
    {
        public void Configure(EntityTypeBuilder<DetailedNotification> builder)
        {
            builder.ToTable("SP_GetNotificationsByUserId").HasNoKey();
            
        }
    }
}
