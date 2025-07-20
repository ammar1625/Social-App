using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SocialAppDataLayer.Dtos;

namespace SocialAppBusinessLayer.Mapping
{
    public class UserBusinessMappingProfile:Profile
    {
        public UserBusinessMappingProfile()
        {
            CreateMap<clsUser, UserDto>().ReverseMap();
        }
    }
}
