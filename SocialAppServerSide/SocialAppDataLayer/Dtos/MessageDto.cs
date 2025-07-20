using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class MessageDto
    {
        public string MessageId { get; set; } = null!;

        public string SenderId { get; set; } = null!;

        public string ConversationId { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string? MessageMediaUrl { get; set; }

        public DateTime SentAt { get; set; }

        public UserDto Sender { get; set; } = null!;

        public MessageDto(string MessageId , string SenderId , string ConversationId , string Content , string? MessageMediaUrl,
            DateTime SentAt)
        {
            this.MessageId = MessageId;
            this.SenderId = SenderId;
            this.ConversationId = ConversationId;
            this.Content = Content;
            this.MessageMediaUrl = MessageMediaUrl;
            this.SentAt = SentAt;
        }

        public MessageDto()
        {
            
        }
    }
}
