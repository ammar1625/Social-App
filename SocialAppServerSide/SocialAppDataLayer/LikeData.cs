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
    public class LikeData
    {
        public static async Task<LikeDto>? GetLikeByIdAsync(string LikeId)
        {
            LikeDto? likeDto = null;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Like? Like = await Context.Likes.FirstOrDefaultAsync(l=>l.LikeId==LikeId);

                    if(Like!= null)
                    {
                        likeDto = MapperConfigData.Mapper.Map<LikeDto>(Like);   
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return likeDto;
        }

        public static async Task<bool> AddNewLikeAsync(LikeDto like)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Like NewLike = MapperConfigData.Mapper.Map<Like>(like);
                    await Context.Likes.AddAsync(NewLike);

                    AffectedRows = await Context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }
            return AffectedRows > 0;
        }

        public static async Task<List<LikeDto>> GetAllLikesByPostIdAsync(string PostId)
        {
            List<LikeDto> Likesdto = new List<LikeDto>();
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    List<Like> Likes = await Context.Likes
                        .Include(l=>l.User)
                        .Where(l=>l.PostId==PostId).ToListAsync();

                    if(Likes.Count>0)
                    {
                        Likesdto = MapperConfigData.Mapper.Map<List<LikeDto>>(Likes);   
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return Likesdto;
        }

        public static async Task<bool> DeleteLikeAsync(string UserId , string PostId)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Like LikeToDelete = await Context.Likes.FirstOrDefaultAsync(l=>l.PostId==PostId && l.UserId==UserId);

                    if (LikeToDelete != null)
                    {
                        Context.Likes.Remove(LikeToDelete);

                        AffectedRows = await Context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return AffectedRows > 0;
        }

        public static async Task<bool> IsLikeExistsAsync(string UserId ,string PostId)
        {
            bool IsExists = false;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    IsExists = await Context.Likes.AnyAsync(l=>l.UserId==UserId && l.PostId==PostId);
                }
                catch (Exception ex)
                {

                    
                }
            }

            return  IsExists;
        }

        
    }
}
