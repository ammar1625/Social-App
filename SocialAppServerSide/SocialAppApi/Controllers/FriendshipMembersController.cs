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
    [Route("api/FriendshipMembers")]
    [ApiController]
    [Authorize]
    
  
    public class FriendshipMembersController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("{FriendshipMemberId}",Name ="GetFriendshipMemberById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFriendshipMemberByIdAsync(string FriendshipMemberId)
        {
            if(FriendshipMemberId == null || string.IsNullOrEmpty(FriendshipMemberId)|| string.IsNullOrWhiteSpace(FriendshipMemberId))
            {
                return BadRequest("invalid data");
            }

            clsFriendshipMember? FriendshipMember = await clsFriendshipMember.GetFriendshipMemberByIdAsync(FriendshipMemberId);

            if(FriendshipMember==null)
            {
                return NotFound($"friendship member with id {FriendshipMemberId} is not found");
            }

            return Ok(FriendshipMember.FriendshipMemberdto);
        }

       
        [HttpPost("add-new")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewFriendshipMemberAsync(FriendshipMemberModel Model)
        {
            if(Model==null|| string.IsNullOrEmpty(Model.FriendshipId)||string.IsNullOrWhiteSpace(Model.FriendshipId)||
                string.IsNullOrEmpty(Model.UserId)||string.IsNullOrWhiteSpace(Model.UserId))
            {
                return BadRequest("invalid data");
            }

            clsFriendshipMember FriendshipMember = new clsFriendshipMember(new FriendshipMemberDto(
                Guid.NewGuid().ToString(),
                Model.FriendshipId,
                Model.UserId));

            if(await FriendshipMember.AddNewFriendshipMemberAsync())
            {
                return CreatedAtRoute("GetFriendshipMemberById", new { FriendShipMemberId = FriendshipMember.FriendShipMemberId},FriendshipMember.FriendshipMemberdto);
            }
            return StatusCode(500,"internal server error");
        }

        [HttpGet("friendslist")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFriendsListByUserId(string UserId)
        {
            if (UserId == null || string.IsNullOrEmpty(UserId) || string.IsNullOrWhiteSpace(UserId))
            {
                return BadRequest("invalid data");
            }

            List<DetailedFriendshipMemberDto> FriendsList = await clsFriendshipMember.GetFriendsListByUserIdAsync(UserId);

            return Ok(FriendsList);
        }

    }
}
