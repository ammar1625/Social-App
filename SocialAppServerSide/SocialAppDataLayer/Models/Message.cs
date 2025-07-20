using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class Message
{
    public string MessageId { get; set; } = null!;

    public string SenderId { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? MessageMediaUrl { get; set; }

    public DateTime SentAt { get; set; }

    public  Conversation Conversation { get; set; } = null!;

    public  User Sender { get; set; } = null!;
}
