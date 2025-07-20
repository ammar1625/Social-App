using AutoMapper;
using SocialAppDataLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer.Mapping
{
    internal class CommentBusinessMappingProfile:Profile
    {
        public CommentBusinessMappingProfile()
        {
            CreateMap<CommentDto,clsComment>().ReverseMap();
        }
    }
}
