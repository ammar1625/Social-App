using Microsoft.EntityFrameworkCore;
using SocialAppDataLayer;
using SocialAppDataLayer.Data;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Mapping;
using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer
{
    public class clsChangePasswordToken
    {
        public string ChangePassWordTokenId { get; set; } = null!;

        public string ChangePassWordToken1 { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string UserId { get; set; } = null!;

        public clsUser User { get; set; } = null!;

        public ChangePassWordTokenDto ChangePassWordTokendto 
        {
            get
            {
                return new ChangePassWordTokenDto(this.ChangePassWordTokenId,this.ChangePassWordToken1,this.CreatedAt,
                    this.ExpirationDate,this.UserId);
            }
        }

        public clsChangePasswordToken()
        {
            
        }

        public clsChangePasswordToken(ChangePassWordTokenDto changepasswordtoken)
        {
            this.ChangePassWordTokenId = changepasswordtoken.ChangePassWordTokenId;
            this.ChangePassWordToken1 = changepasswordtoken.ChangePassWordToken1;
            this.CreatedAt = changepasswordtoken.CreatedAt;
            this.ExpirationDate = changepasswordtoken.ExpirationDate;
            this.UserId = changepasswordtoken.UserId;
        }

        public async Task<bool> AddNewChangePassWordTokenAsync()
        {
            return await ChangePasswordTokenData.AddNewChangePasswordTokenAsync(new ChangePassWordTokenDto(
                this.ChangePassWordTokenId,this.ChangePassWordToken1,this.CreatedAt,this.ExpirationDate,this.UserId));
        }

        public static async Task<clsChangePasswordToken>? GetValidTokenAsync(string Token)
        {
           ChangePassWordTokenDto? tokendto = await ChangePasswordTokenData.GetValidTokenAsync(Token);

           clsChangePasswordToken? token = null;

            if (tokendto != null) 
            {
                token = MapperConfigBusiness.Mapper.Map<clsChangePasswordToken>(tokendto);
            }

            return token;
        }

    }
}
