using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class ChangePassWordToken
{
    public string ChangePassWordTokenId { get; set; } = null!;

    public string ChangePassWordToken1 { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string UserId { get; set; } = null!;

    public  User User { get; set; } = null!;
}
