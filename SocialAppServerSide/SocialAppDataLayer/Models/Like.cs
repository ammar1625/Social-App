using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class Like
{
    public string LikeId { get; set; } = null!;

    public string PostId { get; set; } = null!;

    public DateTime LikedAt { get; set; }

    public string UserId { get; set; } = null!;

    public  Post Post { get; set; } = null!;

    public  User User { get; set; } = null!;
}
