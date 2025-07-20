using AutoMapper;
using SocialAppDataLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer.Mapping
{
    public class ChangePasswordTokenBusinessMappingProfile:Profile
    {
        public ChangePasswordTokenBusinessMappingProfile()
        {
            CreateMap<ChangePassWordTokenDto,clsChangePasswordToken>().ReverseMap();
        }
    }
}
