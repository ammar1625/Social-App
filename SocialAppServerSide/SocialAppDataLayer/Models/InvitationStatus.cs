using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class InvitationStatus
{
    public short StatusId { get; set; }

    public string Status { get; set; } = null!;

    public  ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
}
