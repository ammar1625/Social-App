using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class FriendShipMember
{
    public string FriendShipMemberId { get; set; } = null!;

    public string FriendShipId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public  Friendship FriendShip { get; set; } = null!;

    public  User User { get; set; } = null!;
}
