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
    public class FriendshipData
    {
        public static async Task<FriendshipDto>? GetFriendshipByIdAsync(string FriendshipId)
        {
            FriendshipDto friendshipDto = null;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Friendship? Friendship = await Context.Friendships.FirstOrDefaultAsync(f=>f.FriendShipId==FriendshipId);

                    if(Friendship != null)
                    {
                        friendshipDto = MapperConfigData.Mapper.Map<FriendshipDto>(Friendship);
                    }
                }
                catch (Exception ex)
                {

                }

            }
            return friendshipDto;
        }

        public static async Task<bool> AddNewFriendshipAsync(FriendshipDto Friendship)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Friendship NewFriendship = MapperConfigData.Mapper.Map<Friendship>(Friendship);

                    await Context.Friendships.AddAsync(NewFriendship);

                    AffectedRows = await Context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }

            return AffectedRows > 0;
        }

        public static async Task<bool> IsFriendshipExistsAsync(string CurrentUser , string TargetUser)
        {
            bool IsExists = false;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    List<FriendshipExists> Result = await Context.FriendshipExists.FromSqlInterpolated($"Exec SP_IsFriendShipExists @CurrentUserId = {CurrentUser},@TargetUserId={TargetUser}").ToListAsync();

                    var Response = Result.FirstOrDefault();

                    IsExists = Response.IsFound == 1;
                }
                catch (Exception ex)
                {

                }
            }

            return IsExists;
        }
    }
}
