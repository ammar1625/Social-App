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
    public class DetailedFriendshipMemberConfigs:IEntityTypeConfiguration<DetailedFriendshipMember>
    {
        public void Configure(EntityTypeBuilder<DetailedFriendshipMember>buider)
        {
            buider.ToTable("SP_GetFriendsListByUserId").HasNoKey();
        }
    }
}
