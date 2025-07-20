using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class NotificationType
{
    public short NotificationTypeId { get; set; }

    public string NotificationTypeName { get; set; } = null!;

    public  ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
