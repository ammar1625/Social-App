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
    public class clsLike
    {
        public string LikeId { get; set; } = null!;

        public string PostId { get; set; } = null!;

        public DateTime LikedAt { get; set; }

        public string UserId { get; set; } = null!;

       // public Post Post { get; set; } = null!;

        public clsUser User { get; set; } = null!;

        public LikeDto LikeDto 
        {
            get
            {
                return new LikeDto(this.LikeId,this.PostId,this.LikedAt,this.UserId);
            }

        }

        public clsLike()
        {
            
        }

        public clsLike(LikeDto Like)
        {
            this.LikeId = Like.LikeId;
            this.PostId = Like.PostId;
            this.UserId = Like.UserId;
            this.LikedAt = Like.LikedAt;
        }

        public static async Task<clsLike>? GetLikeByIdAsync(string LikeId)
        {
            LikeDto? LikeDto =  await LikeData.GetLikeByIdAsync(LikeId);
            clsLike Like = null;

            if (LikeDto != null) 
            {
                Like = MapperConfigBusiness.Mapper.Map<clsLike>(LikeDto);
            }
            return Like;
        }

        public  async Task<bool> AddNewLikeAsync()
        {
            return await LikeData.AddNewLikeAsync(new LikeDto(this.LikeId,this.PostId,this.LikedAt,this.UserId));
        }

        public static async Task<List<LikeDto>> GetAllLikesByPostIdAsync(string PostId)
        {
            return await LikeData.GetAllLikesByPostIdAsync(PostId);
        }

        public static async Task<bool> DeleteLikeAsync(string UserId, string PostId)
        {
            return await LikeData.DeleteLikeAsync(UserId,PostId);
        }

        public static async Task<bool> IsLikeExistsAsync(string UserId, string PostId)
        {
            return await LikeData.IsLikeExistsAsync(UserId,PostId);
        }
    }
}
