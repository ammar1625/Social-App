using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class FriendshipDto
    {
        public string FriendShipId { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public FriendshipDto()
        {
            
        }

        public FriendshipDto(string FriendshipId , DateTime CreatedAt)
        {
            this.FriendShipId = FriendshipId;
            this.CreatedAt = CreatedAt;
        }
    }
}
