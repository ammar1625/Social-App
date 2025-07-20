using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Data.Config
{
    public class DetailedConversationMemberConfigs:IEntityTypeConfiguration<DetailedConversationMember>
    {
        public void Configure(EntityTypeBuilder<DetailedConversationMember>builder)
        {
            builder.ToTable("SP_GetConversationsByUserId").HasNoKey();

        }
    }
}
