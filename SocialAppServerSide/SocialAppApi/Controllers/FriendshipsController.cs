using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialAppApi.Models;
using SocialAppBusinessLayer;
using SocialAppDataLayer.Dtos;

namespace SocialAppApi.Controllers
{
    [Route("api/Friendships")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
    
    public class FriendshipsController : ControllerBase
    {
        [HttpGet("{FriendshipId}",Name ="GetFriendshipById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFriendshipByIdAsync(string FriendshipId)
        {
            if(FriendshipId==null || string.IsNullOrEmpty(FriendshipId)||string.IsNullOrWhiteSpace(FriendshipId))
            {
                return BadRequest("invaid data");
            }

            clsFriendship Friendship = await clsFriendship.GetFriendshipByIdAsync(FriendshipId);

            if(Friendship == null) 
                return NotFound($"friendship with id {FriendshipId} is not found");

            return Ok(Friendship.Friendshipdto);
        }

        [HttpPost("add-new")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewFriendshipAsync()
        {
            clsFriendship Friendship = new clsFriendship(new FriendshipDto(Guid.NewGuid().ToString(),DateTime.Now));

            if(await Friendship.AddNewFriendshipAsync())
            {
                return CreatedAtRoute("GetFriendshipById",new {FriendshipId = Friendship.FriendShipId},Friendship.Friendshipdto);
            }

            return StatusCode(500,"internal server error");

        }
        [HttpGet("isFriendshipExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <IActionResult> IsFriendshipExistsAsync([FromQuery] FriendshipExistsModel Model)
        {
            if(Model==null || string.IsNullOrEmpty(Model.CurrentUserId)
                ||string.IsNullOrWhiteSpace(Model.CurrentUserId) 
                || string.IsNullOrEmpty(Model.TargetUserId)
                || string.IsNullOrWhiteSpace(Model.TargetUserId))
            {
                return BadRequest("invalid data");
            }

            bool IsExists = await clsFriendship.IsFriendshipExistsAsync(Model.CurrentUserId,Model.TargetUserId);

            return Ok(IsExists);

        }


    }
}
