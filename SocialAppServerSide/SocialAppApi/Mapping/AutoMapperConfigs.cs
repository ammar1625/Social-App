using AutoMapper;
using SocialAppApi.Models;
using SocialAppBusinessLayer;
namespace SocialAppApi.Mapping
{
    public class AutoMapperConfigs:Profile
    {
        public AutoMapperConfigs()
        {
            CreateMap<UserModel, clsUser>().ReverseMap();
            CreateMap<clsUser,UserResponseDto>().ReverseMap();
        }
    }
}
