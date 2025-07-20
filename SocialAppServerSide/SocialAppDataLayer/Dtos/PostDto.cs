using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class PostDto
    {
        public string PostId { get; set; } = null!;

        public string? Content { get; set; }

        public string? MediaUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; } = null!;

       // public ICollection<Comment> Comments { get; set; } = new List<Comment>();

       // public ICollection<Like> Likes { get; set; } = new List<Like>();

        //public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public UserDto User { get; set; } = null!;

        public PostDto()
        {
            
        }

        public PostDto(string PostId , string Content , string MediaUrl , DateTime CreatedAt , string UserId)
        {
            this.PostId = PostId;
            this.Content = Content;
            this.MediaUrl = MediaUrl;
            this.CreatedAt = CreatedAt;
            this.UserId = UserId;
        }
    }
}
