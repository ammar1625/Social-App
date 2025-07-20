using SocialAppDataLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer
{
    public class clsNotificationType
    {
        public short NotificationTypeId { get; set; }

        public string NotificationTypeName { get; set; } = null!;

        public NotificationTypeDto NotificationTypeDto
        { get
            { return new NotificationTypeDto(this.NotificationTypeId, this.NotificationTypeName);
            }
        }

        public clsNotificationType()
        {
            
        }

        public clsNotificationType(NotificationTypeDto notificationType)
        {
            this.NotificationTypeId = notificationType.NotificationTypeId;
            this.NotificationTypeName = notificationType.NotificationTypeName;
        }
    }
}
