using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class FriendshipMemberDto
    {
        public string FriendShipMemberId { get; set; } = null!;

        public string FriendShipId { get; set; } = null!;

        public string UserId { get; set; } = null!;

       // public Friendship FriendShip { get; set; } = null!;

        public UserDto User { get; set; } = null!;
        public FriendshipMemberDto()
        {
            
        }

        public FriendshipMemberDto(string FriendshipMemberId , string FriendshipId , string UserId )
        {
            this.FriendShipMemberId = FriendshipMemberId;
            this.FriendShipId = FriendshipId;
            this.UserId = UserId;
        }
    }
}
