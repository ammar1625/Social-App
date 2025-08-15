using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SocialAppBusinessLayer;
using Microsoft.AspNetCore.Http.HttpResults;
using SocialAppDataLayer.Dtos;
using SocialAppApi.Models;

namespace SocialAppApi.Controllers
{
    [Route("api/Conversations")]
    [ApiController]
    [Authorize]
  
    public class ConversationsController : ControllerBase
    {
        
        [HttpGet("{ConversationId}",Name ="GetConversationById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetConversationByIdAsync(string ConversationId)
        {
            if(ConversationId==null || string.IsNullOrEmpty(ConversationId)||string.IsNullOrWhiteSpace(ConversationId))
            {
                return BadRequest("invalid input data");
            }

            clsConversation Conversation = await clsConversation.GetConversationByIdAsync(ConversationId);
            if (Conversation == null)
                return NotFound($"conversation with id {ConversationId} is not found");

            return Ok(Conversation.Conversationdto);
        }

        [HttpGet("get-by-members")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>? GetConversationByMembersAsync([FromQuery] ConversationModel Model)
        {
            if(Model==null || string.IsNullOrEmpty(Model.CurrentUserId)||string.IsNullOrWhiteSpace(Model.CurrentUserId)
                || string.IsNullOrEmpty(Model.TargetUserId) || string.IsNullOrWhiteSpace(Model.TargetUserId))
            {
                return BadRequest("invalid data");
            }

            clsConversation? Conversation = await clsConversation.GetConversationByMembersAsync(Model.CurrentUserId,Model.TargetUserId);
            if (Conversation == null)
            {
                return Ok(false);
            }

            return Ok(Conversation.Conversationdto);
        }

        [HttpPost("add-new")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewConversationAsync()
        {
            clsConversation Conversation = new clsConversation(new ConversationDto(Guid.NewGuid().ToString(),DateTime.Now));

            if(await Conversation.AddNewConversationAsync())
            {
                return CreatedAtRoute("GetConversationById",new { ConversationId = Conversation.ConversationId},Conversation.Conversationdto);
            }
            return StatusCode(500,"internal server error");
        }
    }
}
