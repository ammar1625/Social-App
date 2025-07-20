using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class ConversationMember
{
    public string ConversationMemberId { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public  Conversation Conversation { get; set; } = null!;

    public  User User { get; set; } = null!;
}
