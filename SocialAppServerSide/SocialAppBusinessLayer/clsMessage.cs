using SocialAppDataLayer;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer
{
    public class clsMessage
    {
        public string MessageId { get; set; } = null!;

        public string SenderId { get; set; } = null!;

        public string ConversationId { get; set; } = null!;

        public string? Content { get; set; } = null!;

        public string? MessageMediaUrl { get; set; }

        public DateTime SentAt { get; set; }

        public clsUser Sender { get; set; } = null!;

        public MessageDto MessageDto { get; set; }

        public clsMessage() { }

        public clsMessage(MessageDto message)
        {
            this.MessageId = message.MessageId;
            this.SenderId = message.SenderId;
            this.ConversationId = message.ConversationId;
            this.Content = message.Content;
            this.MessageMediaUrl = message.MessageMediaUrl;
            this.SentAt = message.SentAt;
        }

        public  async Task<bool> AddNewMessageAsync()
        {
            return await MessageData.AddNewMessageAsync(new MessageDto(this.MessageId,this.SenderId,this.ConversationId,
                this.Content,this.MessageMediaUrl,this.SentAt));
        }

        public static async Task<List<MessageDto>> GetAllMessagesByConversationIdAsync(string ConversationId)
        {
            List<MessageDto> messageDtos = await MessageData.GetAllMessagesByConversationIdAsync(ConversationId);
            return messageDtos;
        }
    }
}
