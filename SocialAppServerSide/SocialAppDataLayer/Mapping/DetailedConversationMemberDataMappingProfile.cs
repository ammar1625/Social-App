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
    public class DetailedConversationMemberDataMappingProfile:Profile
    {
        public DetailedConversationMemberDataMappingProfile()
        {
            CreateMap<DetailedConversationMember,DetailedConversationMemberDto>().ReverseMap();
        }
    }
}
