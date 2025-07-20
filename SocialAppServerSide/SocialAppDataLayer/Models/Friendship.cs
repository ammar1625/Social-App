using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class Friendship
{
    public string FriendShipId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public  ICollection<FriendShipMember> FriendShipMembers { get; set; } = new List<FriendShipMember>();
}
