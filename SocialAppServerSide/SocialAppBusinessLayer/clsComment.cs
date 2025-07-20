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
    public class clsComment
    {
        public string CommentId { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string PostId { get; set; } = null!;

        public DateTime SentAt { get; set; }

        //public Post Post { get; set; } = null!;

        public clsUser User { get; set; } = null!;

        public CommentDto Commentdto 
        {
            get 
            {
                return new CommentDto(this.CommentId,this.Content,this.UserId,this.PostId,this.SentAt);
            }
        }

        public clsComment()
        {
            
        }

        public clsComment(CommentDto Comment)
        {
            this.CommentId = Comment.CommentId;
            this.Content = Comment.Content;
            this.UserId = Comment.UserId;
            this.PostId = Comment.PostId;
            this.SentAt = Comment.SentAt;
        }

        public static async Task<CommentDto>? GetCommentByIdAsync(string CommentId)
        {
            //CommentDto? commentDto = await CommentData.GetCommentByIdAsync(CommentId);
            //clsComment? Comment = null;
            //if (commentDto != null) 
            //{
            //    Comment = MapperConfigBusiness.Mapper.Map<clsComment>(commentDto);
            //}

            //return Comment;
            return await CommentData.GetCommentByIdAsync(CommentId);
        }

        public  async Task<bool> AddNewCommentAsync()
        {
            return await CommentData.AddNewCommentAsync(new CommentDto(this.CommentId,this.Content,this.UserId,this.PostId,this.SentAt));
        }

        public static async Task<List<CommentDto>> GetCommentsListByPostIdAsync(string PostId)
        {
            return await CommentData.GetCommentsListByPostIdAsync(PostId);
        }
    }
}
