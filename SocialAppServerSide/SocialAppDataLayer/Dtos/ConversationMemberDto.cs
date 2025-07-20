
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class ConversationMemberDto
    {
        public string ConversationMemberId { get; set; } = null!;

        public string ConversationId { get; set; } = null!;

        public string UserId { get; set; } = null!;

        //public Conversation Conversation { get; set; } = null!;

        public UserDto User { get; set; } = null!;

        public ConversationMemberDto()
        {
            
        }

        public ConversationMemberDto(string ConversationMemberId , string ConversationId , string UserId)
        {
            this.ConversationMemberId = ConversationMemberId;
            this.ConversationId = ConversationId;
            this.UserId = UserId;
        }
    }
}
