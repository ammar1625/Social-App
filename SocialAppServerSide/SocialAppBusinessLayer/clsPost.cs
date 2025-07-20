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
    public class clsPost
    {
        public string PostId { get; set; } = null!;

        public string? Content { get; set; }

        public string? MediaUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        public string UserId { get; set; } = null!;

        // public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        // public ICollection<Like> Likes { get; set; } = new List<Like>();

        //public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

        public clsUser User { get; set; } = null!;

        public PostDto PostDto
        {
            get
            {
                return new PostDto(this.PostId,this.Content, this.MediaUrl,this.CreatedAt,this.UserId);
            }
        }
        public clsPost()
        {
            
        }

        public clsPost(PostDto post)
        {
            this.PostId = post.PostId;
            this.Content = post.Content;
            this.MediaUrl = post.MediaUrl;
            this.CreatedAt = post.CreatedAt;
            this.UserId = post.UserId;
        }

        public static async Task<clsPost>? GetPostByidAsync(string PostId)
        {
            PostDto? postdto = await PostData.GetPostByIdAsync(PostId);
            clsPost Post = null;
            if (postdto != null)
            {
                Post = MapperConfigBusiness.Mapper.Map<clsPost>(postdto);
            }

            return Post;
        }

        public async Task<bool> AddNewPostAsync()
        {
            return await PostData.AddNewPostAsync(new PostDto(this.PostId,this.Content,this.MediaUrl,this.CreatedAt,this.UserId));
        }

        public static async Task<List<PostForCurrentUserDto>> GetAllPostsByUserIdAsync(string UserId , string CurrentUserId)
        {
            List<PostForCurrentUserDto> PostsDto = await PostData.GetAllPostsByUserIdAsync(UserId, CurrentUserId);
           // List<clsPost> Posts =  MapperConfigBusiness.Mapper.Map<List<clsPost>>( PostsDto);

            return PostsDto;
        }

        public static async Task<List<PostWithUserAndDetailsDto>> GetAllPostForCurrentUserAndFriendsAsync(string UserId)
        {
            return await PostData.GetAllPostForCurrentUserAndFriendsAsync(UserId);
        }
    }
}
