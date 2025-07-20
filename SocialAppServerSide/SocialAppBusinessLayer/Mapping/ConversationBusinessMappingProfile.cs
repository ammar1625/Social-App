using AutoMapper;
using SocialAppDataLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer.Mapping
{
    public class ConversationBusinessMappingProfile:Profile
    {
        public ConversationBusinessMappingProfile()
        {
            CreateMap<ConversationDto,clsConversation>().ReverseMap();
        }
    }
}
