using AutoMapper;
using SocialAppDataLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer.Mapping
{
    public class MessageBusinessMappingProfile:Profile
    {
        public MessageBusinessMappingProfile()
        {
            CreateMap<MessageDto,clsMessage>().ReverseMap();
        }
    }
}
