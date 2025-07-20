using System;
using System.Collections.Generic;

namespace SocialAppDataLayer.Models;

public partial class Conversation
{
    public string ConversationId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public  ICollection<ConversationMember> ConversationMembers { get; set; } = new List<ConversationMember>();

    public  ICollection<Message> Messages { get; set; } = new List<Message>();
}
