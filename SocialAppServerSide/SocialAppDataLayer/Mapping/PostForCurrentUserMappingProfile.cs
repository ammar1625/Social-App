using AutoMapper;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Mapping
{
    internal class PostForCurrentUserMappingProfile:Profile
    {
        public PostForCurrentUserMappingProfile()
        {
            CreateMap<PostForCurrentUser,PostForCurrentUserDto>().ReverseMap();
        }
    }
}
