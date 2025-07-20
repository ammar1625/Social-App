using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class NotificationTypeDto
    {
        public short NotificationTypeId { get; set; }

        public string NotificationTypeName { get; set; } = null!;

        public NotificationTypeDto()
        {
            
        }
        public NotificationTypeDto(short NotificationTypeId , string NotificationTypeName)
        {
            this.NotificationTypeId = NotificationTypeId;
            this.NotificationTypeName = NotificationTypeName;
        }
    }
}
