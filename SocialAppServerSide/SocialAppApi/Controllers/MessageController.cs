using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SocialAppApi.Models;
using SocialAppBusinessLayer;
using SocialAppDataLayer.Dtos;

namespace SocialAppApi.Controllers
{
    [Route("api/Messages")]
    [ApiController]
    [Authorize]
   
    public class MessageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        public MessageController(IWebHostEnvironment env)
        {
            _env = env;
        }
       
        //[HttpPost("addmessage")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> AddNewMessage(MessageModel Model)
        //{

        //    string? MediaUrl = null;

        //    //handle post picture if any!
        //    if (Model.MessageMedia != null)
        //    {
        //        string FolderName = "Messages-Media";
        //        var UploadsFolder = Path.Combine(_env.WebRootPath, FolderName);

        //        if (!Directory.Exists(UploadsFolder))
        //        {
        //            Directory.CreateDirectory(UploadsFolder);
        //        }

        //        var FileName = Guid.NewGuid() + Path.GetExtension(Model.MessageMedia.FileName);
        //        var FilePath = Path.Combine(UploadsFolder, FileName);

        //        using (FileStream Stream = new FileStream(FilePath, FileMode.Create))
        //        {
        //            await Model.MessageMedia.CopyToAsync(Stream);
        //        }

        //        MediaUrl = $"{Request.Scheme}://{Request.Host}/{FolderName}/{FileName}";
        //    }
        //    clsMessage message = new clsMessage(new MessageDto(Guid.NewGuid().ToString(),Model.SenderId,Model.ConversationId,
        //        Model.Content,MediaUrl,DateTime.Now));

        //    if(await message.AddNewMessageAsync())
        //    {
        //        return Ok("message sent");
        //    }
        //    return StatusCode(500,"internal server error");
        //}

        [HttpPost("save-image")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveImageAsync(IFormFile Image)
        {
            if(Image== null || Image.Length==0)
            {
                return BadRequest("invalid data");
            }

            string FolderName = "Messages-Media";
            var UploadsFolder = Path.Combine(_env.WebRootPath, FolderName);

            if (!Directory.Exists(UploadsFolder))
            {
                Directory.CreateDirectory(UploadsFolder);
            }

            var FileName = Guid.NewGuid() + Path.GetExtension(Image.FileName);
            var FilePath = Path.Combine(UploadsFolder, FileName);

            using (FileStream Stream = new FileStream(FilePath, FileMode.Create))
            {
                await Image.CopyToAsync(Stream);
            }

           string MediaUrl = $"{Request.Scheme}://{Request.Host}/{FolderName}/{FileName}";


            return Ok(MediaUrl);
        }

        [AllowAnonymous]
        [HttpGet("messages/{ConversationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllMessages(string ConversationId)
        {
            List<MessageDto> Messages = await clsMessage.GetAllMessagesByConversationIdAsync(ConversationId);

            return Ok(Messages);
        }
    }
}
