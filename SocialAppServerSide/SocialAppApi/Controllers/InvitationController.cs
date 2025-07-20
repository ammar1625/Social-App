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
    [Route("api/Invitations")]
    [ApiController]
    [Authorize]
    [AllowAnonymous]
  
    public class InvitationController : ControllerBase
    {
        [HttpGet("{InvitationId}",Name ="GetInvitationById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetInvitationByIdAsync(string InvitationId)
        {
            if (InvitationId == null||string.IsNullOrEmpty(InvitationId)|| string.IsNullOrWhiteSpace(InvitationId)) 
            {
                return BadRequest("invalid data");
            }

            clsInvitation? Invitation = await clsInvitation.GetInvitationByIdAsync(InvitationId);

            return Ok(Invitation.Invitationdto);
        }
        [HttpPost("add-new")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewInvitationAsync(InvitationModel Model)
        {
            if (Model == null
                || string.IsNullOrEmpty(Model.SenderId)
                || string.IsNullOrWhiteSpace(Model.SenderId)
                || string.IsNullOrEmpty(Model.RecieverId)
                || string.IsNullOrWhiteSpace(Model.RecieverId)
               ) 
            {
                return BadRequest("invalid data");
            }

            clsInvitation Invitation = new clsInvitation(new InvitationDto(
                Guid.NewGuid().ToString(),
                Model.SenderId,
                Model.RecieverId,
                1,
                DateTime.Now));

            if(await Invitation.AddNewInvitationAsync())
            {
                return CreatedAtRoute("GetInvitationById",new {InvitationId = Invitation.InvitationId},Invitation.Invitationdto);
            }
            return StatusCode(500,"internal server error");
        }

       
        [HttpGet("pending-invitations/{UserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllPendingInvitationsByUserIdAsync(string UserId)
        {
            if(UserId==null || string.IsNullOrEmpty(UserId)||string.IsNullOrWhiteSpace(UserId))
            {
                return BadRequest("invalid data");
            }

            List<InvitationDto> Invitations = await clsInvitation.GetAllPendingInvitationByUserIdAsync(UserId);

            return Ok(Invitations);
        }

        
        [HttpPut("change-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public async Task<IActionResult> ChangeInvitationStatusAsync(ChangeInvitationStatusModel Model)
        {
            if(Model==null || string.IsNullOrEmpty(Model.InvitationId)||string.IsNullOrWhiteSpace(Model.InvitationId)||
                Model.InvitationStatus==null|| Model.InvitationStatus<=0 || Model.InvitationStatus>3)
            {
                return BadRequest("invalid data");
            }

            clsInvitation? Invitation = await clsInvitation.GetInvitationByIdAsync(Model.InvitationId);

            if (Invitation == null) 
            {
                return NotFound($"can not find invitation with id {Model.InvitationId}");
            }

            Invitation.InvitationStatus = Model.InvitationStatus;

            if(await Invitation.ChangeInvitationStatusAsync())
            {
                return Ok(Invitation.Invitationdto);
            }
            return StatusCode(500,"internal server error");
        }

        [HttpGet("isInvitationExists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> IsInvitationExistsAsync([FromQuery] InvitationExistsModel Model)
        {
            if(Model==null||string.IsNullOrEmpty(Model.SenderId)||string.IsNullOrWhiteSpace(Model.SenderId) 
                || string.IsNullOrEmpty(Model.RecieverId) || string.IsNullOrWhiteSpace(Model.RecieverId))
            {
                return BadRequest("invalid data");
            }

            InvitationDto? Invitation = await clsInvitation.IsInvitationExistsAsync(Model.SenderId,Model.RecieverId);
            return Ok(Invitation);
        }

        [HttpDelete("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteInvitationAsync(InvitationExistsModel Model)
        {
            if (Model == null || string.IsNullOrEmpty(Model.SenderId) || string.IsNullOrWhiteSpace(Model.SenderId)
               || string.IsNullOrEmpty(Model.RecieverId) || string.IsNullOrWhiteSpace(Model.RecieverId))
            {
                return BadRequest("invalid data");
            }

            InvitationDto Invitation = await clsInvitation.IsInvitationExistsAsync(Model.SenderId,Model.RecieverId);

            if(Invitation==null)
            {
                return NotFound("content is not found");
            }

            if(await clsInvitation.DeleteInvitationASync(Model.SenderId,Model.RecieverId))
            {
                return Ok(true);
            }
            return Ok(false);

        }

    }
}
