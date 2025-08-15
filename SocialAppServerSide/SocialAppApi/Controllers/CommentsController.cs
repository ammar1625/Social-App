using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialAppApi.Models;
using SocialAppBusinessLayer;
using SocialAppDataLayer.Dtos;

namespace SocialAppApi.Controllers
{
    [Route("api/Comments")]
    [ApiController]
    [Authorize]
   
    public class CommentsController : ControllerBase
    {
        [HttpGet("{CommentId}",Name ="GetCommentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCommentByIdAsync(string CommentId)
        {
            if(CommentId==null||string.IsNullOrEmpty(CommentId)||string.IsNullOrWhiteSpace(CommentId))
            {
                return BadRequest("invalid data");
            }

            CommentDto? Comment = await clsComment.GetCommentByIdAsync(CommentId);
            if (Comment == null)
                return NotFound($"comment with id {CommentId} is not found");
            else
                return Ok(Comment);
        }

        [HttpPost( "add-new")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewCommentAsync(CommentModel Model)
        {
            if(Model==null|| string.IsNullOrEmpty(Model.Content)||string.IsNullOrWhiteSpace(Model.Content)||
                string.IsNullOrEmpty(Model.UserId) || string.IsNullOrWhiteSpace(Model.UserId)||
                string.IsNullOrEmpty(Model.PostId) || string.IsNullOrWhiteSpace(Model.PostId))
            {
                return BadRequest("invalid data");
            }

            clsComment Comment = new clsComment
                (new CommentDto(Guid.NewGuid().ToString(),Model.Content,Model.UserId,Model.PostId,DateTime.Now));

            if(await Comment.AddNewCommentAsync())
            {
                return CreatedAtRoute("GetCommentById",new {CommentId = Comment.CommentId},Comment.Commentdto);
            }
            return StatusCode(500,"internal server error");
        }

        [HttpGet("comments-list/{PostId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCommentsListByPostIdAsync(string PostId)
        {
            if(PostId==null||string.IsNullOrEmpty(PostId)||string.IsNullOrWhiteSpace(PostId))
            {
                return BadRequest("invalid data");
            }

            List<CommentDto> Comments = await clsComment.GetCommentsListByPostIdAsync(PostId);
            return Ok(Comments);
        }

    }
}
