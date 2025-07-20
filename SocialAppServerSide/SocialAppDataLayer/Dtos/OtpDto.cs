using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class OtpDto
    {
        public string Id { get; set; } = null!;

        public int Code { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string UserId { get; set; } = null!;

        public bool IsUsed { get; set; }

        public UserDto User { get; set; } = null!;

        public OtpDto()
        {
            
        }

        public OtpDto(string Id , int Code , DateTime CreatedAt , DateTime ExpirationDate , string UserId , bool IsUsed)
        {
            this.Id = Id;
            this.Code = Code;
            this.CreatedAt = CreatedAt;
            this.ExpirationDate = ExpirationDate;
            this.UserId = UserId;
            this.IsUsed = IsUsed;
        }
    }
}
