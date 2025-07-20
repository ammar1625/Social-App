using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class Post
{
    public string PostId { get; set; } = null!;

    public string? Content { get; set; }

    public string? MediaUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public string UserId { get; set; } = null!;

    public  ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public  ICollection<Like> Likes { get; set; } = new List<Like>();

    public  ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public  User User { get; set; } = null!;
}
