using AutoMapper;
using SocialAppDataLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer.Mapping
{
    public class ConversationMemberBusinessMappingProfile:Profile
    {
        public ConversationMemberBusinessMappingProfile()
        {
            CreateMap<ConversationMemberDto , clsConversationMember>().ReverseMap();
        }
    }
}
