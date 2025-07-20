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
    internal class PostsWithUserAndDetaisConfigs : IEntityTypeConfiguration<PostWithUserAndDetails>
    {
        public void Configure(EntityTypeBuilder<PostWithUserAndDetails> builder)
        {
            builder.ToTable("Sp_GetAllPosts").HasNoKey();
           
        }
    }
}
