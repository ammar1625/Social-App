using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class RefreshTokenDto
    {
        public string RefreshTokenId { get; set; } = null!;

        public string RefreshToken1 { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string UserId { get; set; } = null!;

        public UserDto User { get; set; } = null!;

        public RefreshTokenDto()
        {
            
        }

        public RefreshTokenDto(string RefreshTokenId , string RefreshToken , DateTime CreatedAt , DateTime ExpirationDate,
            string UserId)
        {
            this.RefreshTokenId = RefreshTokenId;
            this.RefreshToken1 = RefreshToken;
            this.CreatedAt = CreatedAt;
            this.ExpirationDate = ExpirationDate;
            this.UserId = UserId;
        }
    }
}
