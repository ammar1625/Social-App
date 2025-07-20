using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class DetailedNotificationDto
    {
        public string NotificationId { get; set; } = null!;

        public string? Content { get; set; }

        public string PostId { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public short NotificationTypeId { get; set; }

        public DateTime NotificationDate { get; set; }

        public bool IsRead { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string ProfilePic { get; set; } = null!;
        public char Gender { get; set; }

        public DetailedNotificationDto()
        {
            
        }

       
    }
}
