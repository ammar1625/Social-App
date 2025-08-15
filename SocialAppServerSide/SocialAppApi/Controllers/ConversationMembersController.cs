using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialAppApi.Models;
using SocialAppBusinessLayer;
using SocialAppBusinessLayer.Mapping;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Mapping;

namespace SocialAppApi.Controllers
{
    [Route("api/ConverastionMembers")]
    [ApiController]
    [Authorize]
   
    public class ConversationMembersController : ControllerBase
    {
       
        [HttpGet("{ConversationMemberId}",Name = "GetConversationMemberById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetConversationMemberByIdAsync(string ConversationMemberId)
        {
            if(ConversationMemberId == null
               || string.IsNullOrEmpty(ConversationMemberId)
               || string.IsNullOrWhiteSpace(ConversationMemberId))
            {
                return BadRequest("invalid data");
            }

            clsConversationMember ConversationMember = await clsConversationMember.GetConversationMemberByIdAsync(ConversationMemberId);

            if (ConversationMember == null) 
            {
                return NotFound($"conversation member with id {ConversationMemberId} is not found");
            }
           // ConversationMember.ConversationMemberdto.User = MapperConfigBusiness.Mapper.Map<UserDto>(ConversationMember.User);
            return Ok(ConversationMember);
        }
        [HttpPost("add-new")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewConevrsationMemberAsync(ConversationMemberModel Model)
        {
                if(Model==null||
                string.IsNullOrEmpty(Model.ConversationId)||
                string.IsNullOrWhiteSpace(Model.ConversationId) ||
                 string.IsNullOrEmpty(Model.UserId) ||
                string.IsNullOrWhiteSpace(Model.ConversationId) )
                {
                    return BadRequest("invalid data");
                }

            clsConversationMember ConversationMember = new clsConversationMember
                (
                new ConversationMemberDto(Guid.NewGuid().ToString(),Model.ConversationId, Model.UserId)
                );

             if(await ConversationMember.AddNewConversationMemberAsync())
            {
                return CreatedAtRoute("GetConversationMemberById", new {ConversationMemberId = ConversationMember.ConversationMemberId},ConversationMember.ConversationMemberdto);
            }
            return StatusCode(500,"internal server error");
        }
      
        [HttpGet("conversations-list/{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetConversationsListByUserIdAsync(string UserId)
        {
            if(UserId==null|| string.IsNullOrEmpty(UserId)||string.IsNullOrWhiteSpace(UserId))
            {
                return BadRequest("invalid data");
            }
             List<DetailedConversationMemberDto> ConversationsList = await clsConversationMember.GetConversationsListByUserIdAsync(UserId);

            return Ok(ConversationsList);
        }
    }
}
