using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class Comment
{
    public string CommentId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string PostId { get; set; } = null!;

    public DateTime SentAt { get; set; }

    public  Post Post { get; set; } = null!;

    public  User User { get; set; } = null!;
}
