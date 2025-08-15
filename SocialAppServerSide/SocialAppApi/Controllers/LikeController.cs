using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialAppApi.Models;
using SocialAppBusinessLayer;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Models;

namespace SocialAppApi.Controllers
{
    [Route("api/Like")]
    [ApiController]
    [Authorize]
   
    public class LikeController : ControllerBase
    {
       
        [HttpGet("{LikeId}",Name = "GetLikeById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLikeByIdAsync(string LikeId)
        {
            if (LikeId == null || string.IsNullOrEmpty(LikeId)|| string.IsNullOrWhiteSpace(LikeId))
            {
                return BadRequest("invalid data");
            }

            clsLike? Like = await clsLike.GetLikeByIdAsync(LikeId);

            return Ok(Like.LikeDto);
        }
       
        [HttpPost("add-new")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewLikeAsync(LikeModel Model)
        {
            if (Model == null || string.IsNullOrEmpty(Model.PostId)|| string.IsNullOrWhiteSpace(Model.PostId)||
                string.IsNullOrEmpty(Model.UserId)|| string.IsNullOrWhiteSpace(Model.UserId))
            {
                return BadRequest("invalid data");
            }

            clsLike Like = new clsLike(new LikeDto(Guid.NewGuid().ToString(),Model.PostId,DateTime.Now,Model.UserId));

            if(await Like.AddNewLikeAsync())
            {
                return CreatedAtRoute("GetLikeById",new {LikeId = Like.LikeId},Like.LikeDto);
            }

            return StatusCode(500,"internal server error");
        }
      
        [HttpGet("Likes-list/{PostId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLikesByPostId(string PostId)
        {
            if (PostId == null || string.IsNullOrEmpty(PostId) || string.IsNullOrWhiteSpace(PostId))
            {
                return BadRequest("invalid data");
            }

            List<LikeDto> Likes = await clsLike.GetAllLikesByPostIdAsync(PostId);

            return Ok(Likes);
        }
       
        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteLikeAsync(DeleteLikeModel Model)
        {
            if (Model == null || string.IsNullOrEmpty(Model.PostId) || string.IsNullOrWhiteSpace(Model.PostId) ||
              string.IsNullOrEmpty(Model.UserId) || string.IsNullOrWhiteSpace(Model.UserId))
            {
                return BadRequest("invalid data");
            }

            if(await clsLike.DeleteLikeAsync(Model.UserId,Model.PostId))
            {
                return Ok(true);
            }

            return Ok(false);
        }
        
        [HttpGet("isLiked")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IsLikeExistsAsync([FromQuery] LikeModel Model)
        {
            if (Model == null || string.IsNullOrEmpty(Model.PostId) || string.IsNullOrWhiteSpace(Model.PostId) ||
              string.IsNullOrEmpty(Model.UserId) || string.IsNullOrWhiteSpace(Model.UserId))
            {
                return BadRequest("invalid data");
            }

            bool IsLiked = await clsLike.IsLikeExistsAsync(Model.UserId,Model.PostId);

            return Ok(IsLiked);
        }
    }
}
