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
    public class PostData
    {
        public static async Task<PostDto>? GetPostByIdAsync(string PostId)
        {
            PostDto postDto = null;
            using(AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Post? Post = await Context.Posts.FirstOrDefaultAsync(p=>p.PostId==PostId);

                    if(Post != null)
                    {
                        postDto = MapperConfigData.Mapper.Map<PostDto>(Post);
                    }
                }
                catch (Exception ex) 
                {

                }
            }

            return postDto;
        }

        public static async Task<bool> AddNewPostAsync(PostDto NewPost)
        {
            int AffectedRows = 0;
            using(AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Post Post = MapperConfigData.Mapper.Map<Post>(NewPost); 

                    await Context.Posts.AddAsync(Post);

                    AffectedRows = await Context.SaveChangesAsync();
                }
                catch (Exception ex) 
                {

                }
            }

            return AffectedRows > 0;
        }

        public static async Task<List<PostForCurrentUserDto>> GetAllPostsByUserIdAsync(string UserId , string CurrentUserId)
        {
            List<PostForCurrentUserDto> posts = new List<PostForCurrentUserDto>();
            using(AppDbContext Context = new AppDbContext())
            {
                try
                {
                    List<PostForCurrentUser> Posts = await Context.CurrentUserPosts.
                        FromSqlInterpolated($"Exec SP_GetAllPostForCurrentUser @UserId = {UserId}, @CurrentUserId = {CurrentUserId}").ToListAsync();

                    if(Posts.Count>0)
                    {
                        //Posts.ForEach(p=>posts.Add(MapperConfigData.Mapper.Map<PostDto>(p)));
                        posts = MapperConfigData.Mapper.Map<List<PostForCurrentUserDto>>(Posts);
                    }
                }
                catch(Exception ex)
                {

                }
            }
            return posts;
        }

        public static async Task<List<PostWithUserAndDetailsDto>> GetAllPostForCurrentUserAndFriendsAsync(string UserId)
        {
            List<PostWithUserAndDetailsDto> PostsDto = new List<PostWithUserAndDetailsDto>();
            using(AppDbContext Context = new AppDbContext())
            {
                try
                {
                    List<PostWithUserAndDetails> Posts = await Context.AllPosts.FromSqlInterpolated($"Exec Sp_GetAllPosts @UserId = {UserId}")
                        .ToListAsync();

                    if (Posts.Count > 0)
                    {
                        PostsDto = MapperConfigData.Mapper.Map<List<PostWithUserAndDetailsDto>>(Posts);
                    }
                }
                catch (Exception ex) 
                {

                }
            }

            return PostsDto;
        }
    }
}
