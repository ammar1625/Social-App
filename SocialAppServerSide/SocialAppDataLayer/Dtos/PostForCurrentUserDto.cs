using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class PostForCurrentUserDto
    {
        public string PostId { get; set; } = null!;

        public string? Content { get; set; }

        public string? MediaUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = null!;

        public string PassWord { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? ProfilePic { get; set; }

        public char Gender { get; set; }

        public bool IsEmailVerified { get; set; }

        public bool IsActive { get; set; }

        public int CommentsCount { get; set; }
        public int LikesCount { get; set; }
        public string IsLiked { get; set; }

        public PostForCurrentUserDto()
        {
            
        }
    }
}
