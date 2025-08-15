using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialAppApi.Models;
using SocialAppBusinessLayer;
using SocialAppDataLayer;
using SocialAppDataLayer.Dtos;

namespace SocialAppApi.Controllers
{
    [Route("api/Notifications")]
    [ApiController]
    [Authorize]
   
    public class NotificationController : ControllerBase
    {
        
        [HttpGet("{NotificationId}",Name ="GetNotificationById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNotificationByIdAsync(string NotificationId)
        {
            if(NotificationId==null || string.IsNullOrEmpty(NotificationId)|| string.IsNullOrWhiteSpace(NotificationId))
            {
                return BadRequest("invalid data");
            }

            clsNotification? Notification = await clsNotification.GetNotificationByIdAsync(NotificationId);

            return Ok(Notification.NotificationDto);
        }
       
        [HttpPost("add-notification")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult>AddNewNotificationAsync(NewNotificationModel Model)
        {
                if(Model == null
                || string.IsNullOrEmpty(Model.PostId)
                || string.IsNullOrWhiteSpace(Model.PostId)
                || string.IsNullOrEmpty(Model.UserId)
                || string.IsNullOrWhiteSpace(Model.UserId)
                || Model.NotificationTypeId <= 0)
                {
                    return BadRequest("invalid data");
                }

                clsNotification Notification = new clsNotification(new NotificationDto(Guid.NewGuid().ToString(),Model.Content,
                    Model.PostId,Model.UserId,Model.NotificationTypeId,DateTime.Now,false));

               if(await Notification.AddNewNotificationAsync())
                {
                    return CreatedAtRoute("GetNotificationById",new {NotificationId = Notification.NotificationId},Notification.NotificationDto);
                }
            return StatusCode(500, "internal server error");

        }
        
        [HttpGet("unread-notifications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUnreadNotifications(string UserId)
        {
            if (UserId == null || string.IsNullOrEmpty(UserId) || string.IsNullOrWhiteSpace(UserId))
            {
                BadRequest("invalid data");
            }

            List<DetailedNotificationDto> Notifications = await clsNotification.GetAllUnreadNotificationsAsync(UserId);

            return Ok(Notifications);
        }
        [HttpGet("unread-notifications-count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUnreadNotificationsCount(string UserId)
        {
            if (UserId == null || string.IsNullOrEmpty(UserId) || string.IsNullOrWhiteSpace(UserId))
            {
                BadRequest("invalid data");
            }

            int Count = await clsNotification.GetUnreadNotificationsCountAsync(UserId);

            return Ok(Count);
        }

        [HttpPut("change-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeNotificationStatusAsync(string NotificationId)
        {
            if(NotificationId==null||string.IsNullOrEmpty(NotificationId)||string.IsNullOrWhiteSpace(NotificationId))
            {
                return BadRequest("invalid data");
            }

            clsNotification Notification =await clsNotification.GetNotificationByIdAsync(NotificationId);

            if (Notification == null)
                return NotFound($"notification with id {NotificationId} is not found");
            //Notification.IsRead = true;
            if(await Notification.ChangeNotificationStatusAsync())
            {
                return Ok(true);
            }
            return StatusCode(500,"internal server error");
        }


    }
   
    
}
