using AutoMapper;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Models;

namespace SocialAppApi.Models
{
    public class DetailedFriendshipMemberDataMappingProfile:Profile
    {
        public DetailedFriendshipMemberDataMappingProfile()
        {
            CreateMap<DetailedFriendshipMember,DetailedFriendshipMemberDto>().ReverseMap();
        }
    }
}
