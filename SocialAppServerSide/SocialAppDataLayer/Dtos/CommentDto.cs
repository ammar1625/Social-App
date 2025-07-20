
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class CommentDto
    {
        public string CommentId { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string PostId { get; set; } = null!;

        public DateTime SentAt { get; set; }

        //public Post Post { get; set; } = null!;

        public UserDto User { get; set; } = null!;

        public CommentDto(string CommentId , string Content,string UserId,string PostId,DateTime SentAt)
        {
            this.CommentId = CommentId;
            this.Content = Content;
            this.UserId = UserId;
            this.PostId = PostId;
            this.SentAt = SentAt;
        }
        public CommentDto()
        {
            
        }
    }
}
