using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class Invitation
{
    public string InvitationId { get; set; } = null!;

    public string SenderId { get; set; } = null!;

    public string RecieverId { get; set; } = null!;

    public short InvitationStatus { get; set; }

    public DateTime SentAt { get; set; }

    public  InvitationStatus InvitationStatusNavigation { get; set; } = null!;

    public  User Reciever { get; set; } = null!;

    public  User Sender { get; set; } = null!;
}
