using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class Notification
{
    public string NotificationId { get; set; } = null!;

    public string? Content { get; set; }

    public string PostId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public short NotificationTypeId { get; set; }

    public DateTime NotificationDate { get; set; }

    public bool IsRead { get; set; }

    public  NotificationType NotificationType { get; set; } = null!;

    public  Post Post { get; set; } = null!;

    public  User User { get; set; } = null!;
}
