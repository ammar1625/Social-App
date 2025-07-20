using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class ChangePassWordTokenDto
    {
        public string ChangePassWordTokenId { get; set; } = null!;

        public string ChangePassWordToken1 { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string UserId { get; set; } = null!;

        public UserDto User { get; set; } = null!;

        public ChangePassWordTokenDto()
        {
            
        }

        public ChangePassWordTokenDto(string ChangePassWordTokenId ,string ChangePassWordToken1,DateTime CreatedAt,
            DateTime ExpirationDate , string UserId )
        {
            this.ChangePassWordTokenId = ChangePassWordTokenId;
            this.ChangePassWordToken1 = ChangePassWordToken1;
            this.CreatedAt = CreatedAt;
            this.ExpirationDate = ExpirationDate;
            this.UserId = UserId;
        }
    }
}
