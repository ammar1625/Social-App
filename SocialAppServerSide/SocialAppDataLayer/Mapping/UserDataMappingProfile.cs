using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Models;

namespace SocialAppDataLayer.Mapping
{
    public class UserDataMappingProfile:Profile
    {
        public UserDataMappingProfile()
        {
            CreateMap<User,UserDto>().ReverseMap();
        }
    }
}
