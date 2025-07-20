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
    public class CommentData
    {
        public static async Task<CommentDto>? GetCommentByIdAsync(string CommentId)
        {
            CommentDto commentDto = null;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Comment? comment = await Context.Comments
                        .Include(c=>c.User)
                        .FirstOrDefaultAsync(c=>c.CommentId==CommentId);
                    if (comment != null)
                    {
                        commentDto = MapperConfigData.Mapper.Map<CommentDto>(comment);  
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return commentDto;
        }

        public static async Task<bool> AddNewCommentAsync(CommentDto Comment)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Comment NewComment = MapperConfigData.Mapper.Map<Comment>(Comment);

                    await Context.Comments.AddAsync(NewComment);
                    AffectedRows = await Context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }
            return AffectedRows > 0;
        }

        public static async Task<List<CommentDto>> GetCommentsListByPostIdAsync(string PostId)
        {
            List<CommentDto> Commentsdto = new();
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    List<Comment> Comments = await Context.Comments
                        .Include(c=>c.User)
                        .Where(c=>c.PostId==PostId)
                        .OrderByDescending(c=>c.SentAt).ToListAsync();
                    if(Comments.Count>0)
                    {
                        Commentsdto = MapperConfigData.Mapper.Map<List<CommentDto>>(Comments);  
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return Commentsdto;
        }
    }
}
