using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class LikeDto
    {
        public string LikeId { get; set; } = null!;

        public string PostId { get; set; } = null!;

        public DateTime LikedAt { get; set; }

        public string UserId { get; set; } = null!;

        //public Post Post { get; set; } = null!;

        public UserDto User { get; set; } = null!;

        public LikeDto()
        {
            
        }

        public LikeDto(string LikeId ,string PostId , DateTime LikedAt , string UserId)
        {
            this.LikeId = LikeId;
            this.PostId = PostId;
            this.LikedAt = LikedAt;
            this.UserId = UserId;
        }
    }
}
