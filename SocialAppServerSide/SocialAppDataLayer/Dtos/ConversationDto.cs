using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class ConversationDto
    {
        public string ConversationId { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public ConversationDto()
        {
            
        }

        public ConversationDto(string ConversationId , DateTime CreatedAt)
        {
            this.ConversationId = ConversationId;
            this.CreatedAt = CreatedAt;
        }
    }
}
