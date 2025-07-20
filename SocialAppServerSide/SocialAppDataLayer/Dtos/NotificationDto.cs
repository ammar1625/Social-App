using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class NotificationDto
    {
        public string NotificationId { get; set; } = null!;

        public string? Content { get; set; }

        public string PostId { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public short NotificationTypeId { get; set; }

        public DateTime NotificationDate { get; set; }

        public bool IsRead { get; set; }

        public NotificationTypeDto NotificationType { get; set; } = null!;

        public PostDto Post { get; set; } = null!;

        public UserDto User { get; set; } = null!;

        public NotificationDto()
        {
            
        }

        public NotificationDto(string NotificationId, string? Content , string PostId, string UserId, short NotificationTypeId,
            DateTime NotificationDate, bool IsRead)
        {
            this.NotificationId = NotificationId;
            this.Content = Content;
            this.PostId = PostId;
            this.UserId = UserId;
            this.NotificationTypeId = NotificationTypeId;
            this.NotificationDate = NotificationDate;
            this.IsRead = IsRead;
        }
    }
}
