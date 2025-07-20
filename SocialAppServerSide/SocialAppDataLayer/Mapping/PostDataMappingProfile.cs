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
    internal class PostDataMappingProfile:Profile
    {
        public PostDataMappingProfile()
        {
            CreateMap<Post,PostDto>().ReverseMap();
        }
    }
}
