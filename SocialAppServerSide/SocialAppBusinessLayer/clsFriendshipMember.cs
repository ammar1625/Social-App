using SocialAppDataLayer;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer
{
    public class clsFriendshipMember
    {
        public string FriendShipMemberId { get; set; } = null!;

        public string FriendShipId { get; set; } = null!;

        public string UserId { get; set; } = null!;

        //public Friendship FriendShip { get; set; } = null!;

        public clsUser User { get; set; } = null!;

        public FriendshipMemberDto FriendshipMemberdto 
        {
            get { return new FriendshipMemberDto(this.FriendShipMemberId, this.FriendShipId, this.UserId); }
        }

        public clsFriendshipMember()
        {
            
        }

        public clsFriendshipMember(FriendshipMemberDto Friendshipmember)
        {
            this.FriendShipMemberId = Friendshipmember.FriendShipMemberId;
            this.FriendShipId = Friendshipmember.FriendShipId;
            this.UserId = Friendshipmember.UserId;
        }

        public static async Task<clsFriendshipMember>? GetFriendshipMemberByIdAsync(string FriendshipMemberId)
        {
            FriendshipMemberDto? friendshipMemberDto = await FriendshipMemberData
                .GetFriendshipMemberByIdAsync(FriendshipMemberId);
            clsFriendshipMember? Friendshipmember = null;

            if(friendshipMemberDto != null)
            {
                Friendshipmember = MapperConfigBusiness.Mapper.Map<clsFriendshipMember>(friendshipMemberDto);
            }

            return Friendshipmember;
        }

        public async Task<bool> AddNewFriendshipMemberAsync()
        {
            return await FriendshipMemberData
                .AddNewFriendshipMemberAsync(new FriendshipMemberDto(this.FriendShipMemberId,this.FriendShipId,this.UserId));
        }

        public static async Task<List<DetailedFriendshipMemberDto>> GetFriendsListByUserIdAsync(string UserId)
        {
            return await FriendshipMemberData.GetFriendsListByUserIdAsync(UserId);
        }
    }
}
