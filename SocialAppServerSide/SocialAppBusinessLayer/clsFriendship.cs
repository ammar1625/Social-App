using SocialAppDataLayer;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Mapping;
using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer
{
    public class clsFriendship
    {
        public string FriendShipId { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public FriendshipDto Friendshipdto
        {
            get
            {
                return new FriendshipDto(this.FriendShipId,this.CreatedAt);
            }
        }

        public clsFriendship()
        {
            
        }

        public clsFriendship(FriendshipDto Friendship)
        {
            this.FriendShipId = Friendship.FriendShipId;
            this.CreatedAt = Friendship.CreatedAt;
        }

        public static async Task<clsFriendship>? GetFriendshipByIdAsync(string FriendshipId)
        {
            FriendshipDto? friendshipDto = await FriendshipData.GetFriendshipByIdAsync(FriendshipId);
            clsFriendship Friendship = null;
            if (friendshipDto != null)
            {
                Friendship = MapperConfigBusiness.Mapper.Map<clsFriendship>(friendshipDto);
            }
            return Friendship;
        }

        public  async Task<bool> AddNewFriendshipAsync()
        {
            return await FriendshipData.AddNewFriendshipAsync(new FriendshipDto(this.FriendShipId,this.CreatedAt));
        }

        public static async Task<bool> IsFriendshipExistsAsync(string CurrentUser, string TargetUser)
        {
            return await FriendshipData.IsFriendshipExistsAsync(CurrentUser,TargetUser);
        }
    }
}
