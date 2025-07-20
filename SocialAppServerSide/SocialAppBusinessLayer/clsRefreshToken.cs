using SocialAppDataLayer;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer
{
    public class clsRefreshToken
    {
        public string RefreshTokenId { get; set; } = null!;

        public string RefreshToken1 { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string UserId { get; set; } = null!;

        public clsUser User { get; set; } = null!;

        public RefreshTokenDto RefreshTokendto { 
            get
            {
                return new RefreshTokenDto(this.RefreshTokenId , this.RefreshToken1 , this.CreatedAt,this.ExpirationDate,
                    this.UserId);
            }
        }

        public clsRefreshToken()
        {
            
        }

        public clsRefreshToken(RefreshTokenDto refreshtoken)
        {
            this.RefreshTokenId = refreshtoken.RefreshTokenId;
            this.RefreshToken1 = refreshtoken.RefreshToken1;
            this.CreatedAt = refreshtoken.CreatedAt;
            this.ExpirationDate = refreshtoken.ExpirationDate;
            this.UserId = refreshtoken.UserId;
        }

        public async Task<bool> AddNewRefreshTokenAsync()
        {
            return await RefreshTokenData.AddNewRefreshTokenAsync(new RefreshTokenDto(this.RefreshTokenId,this.RefreshToken1,
                this.CreatedAt,this.ExpirationDate,this.UserId));
        }

        public static async Task<bool> DeleteAllRefreshTokensByUserIdAsync(string UserId)
        {
            return await RefreshTokenData.DeleteAllRefreshTokenByUserIdAsync(UserId);
        }

        public static async Task<clsRefreshToken>? GetValidRefreshTokenAsync(string Token)
        {
            RefreshTokenDto? reftokendto =await RefreshTokenData.GetValidRefreshTokenAsync(Token);
            clsRefreshToken? RefreshToken = null;
            if(reftokendto != null)
            {
                RefreshToken = MapperConfigBusiness.Mapper.Map<clsRefreshToken>(reftokendto);
            }

            return RefreshToken;
        }

    }
}
