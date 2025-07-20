using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialAppDataLayer.Data;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Mapping;
using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer
{
    public class FriendshipMemberData
    {
        public static async Task<FriendshipMemberDto>? GetFriendshipMemberByIdAsync(string FriendshipMemberId)
        {
            FriendshipMemberDto friendshipMemberdto = null;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    FriendShipMember? Friendshipmember = await Context.FriendShipMembers
                        .FirstOrDefaultAsync(f=>f.FriendShipMemberId==FriendshipMemberId);

                    if (Friendshipmember != null) 
                    {
                        friendshipMemberdto = MapperConfigData.Mapper.Map<FriendshipMemberDto>(Friendshipmember);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return friendshipMemberdto;
        }

        public static async Task<bool> AddNewFriendshipMemberAsync(FriendshipMemberDto Friendshipmember)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    FriendShipMember NewFriendshipMember = MapperConfigData.Mapper.Map<FriendShipMember>(Friendshipmember);

                    await Context.FriendShipMembers.AddAsync(NewFriendshipMember);

                    AffectedRows = await Context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }

            return AffectedRows > 0;
        }

        public static async Task<List<DetailedFriendshipMemberDto>>GetFriendsListByUserIdAsync(string UserId)
        {
            List<DetailedFriendshipMemberDto> FriendsListDtos = new List<DetailedFriendshipMemberDto>();
            using (AppDbContext Context= new AppDbContext())
            {
                try
                {
                    List<DetailedFriendshipMember> FriendshipMembers = await Context.FriendsList
                        .FromSqlInterpolated($"Exec SP_GetFriendsListByUserId @UserId={UserId}").ToListAsync();

                    if( FriendshipMembers.Count>0 )
                    FriendsListDtos = MapperConfigData.Mapper.Map<List<DetailedFriendshipMemberDto>>(FriendshipMembers);
                }
                catch (Exception ex)
                {

                }
            }

            return FriendsListDtos;
        }
    }
}
