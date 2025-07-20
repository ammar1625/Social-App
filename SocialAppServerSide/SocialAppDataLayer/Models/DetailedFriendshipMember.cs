using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Models
{
    public class DetailedFriendshipMember
    {
        public string FriendShipMemberId { get; set; } = null!;
        public string UserId { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = null!;

        public string PassWord { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? ProfilePic { get; set; }

        public bool IsEmailVerified { get; set; }

        public bool IsActive { get; set; }
        public char Gender { get; set; }
        public string FriendShipId { get; set; } = null!;


    }
}
