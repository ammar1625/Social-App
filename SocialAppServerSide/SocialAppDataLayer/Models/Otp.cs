using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class Otp
{
    public string Id { get; set; } = null!;

    public int Code { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime ExpirationDate { get; set; }

    public string UserId { get; set; } = null!;

    public bool IsUsed { get; set; }

    public  User User { get; set; } = null!;
}
