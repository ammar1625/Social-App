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
    public class clsConversation
    {
        public string ConversationId { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        public ConversationDto Conversationdto 
        {
            get 
            {
                return new ConversationDto(this.ConversationId,this.CreatedAt);
            }
        }

        public clsConversation()
        {
            
        }

        public clsConversation(ConversationDto Conversation)
        {
            this.ConversationId = Conversation.ConversationId;
            this.CreatedAt = Conversation.CreatedAt;
        }

        public static async Task<clsConversation>? GetConversationByIdAsync(string ConversationId)
        {
            ConversationDto? Conversationdto = await ConversationData.GetConversationByIdAsync(ConversationId);
            clsConversation Conversation = null;
            if(Conversationdto != null)
            {
                Conversation = MapperConfigBusiness.Mapper.Map<clsConversation>(Conversationdto);
            }
            return Conversation;
        }

        public static async Task<clsConversation>? GetConversationByMembersAsync(string CurrentUserId, string TargetUserId)
        {
            ConversationDto? conversationDto = await ConversationData.GetConversationByMembersAsync(CurrentUserId, TargetUserId);
            clsConversation? conversation = null;
            if (conversationDto != null)
            {
                conversation = MapperConfigBusiness.Mapper.Map<clsConversation>(conversationDto);
            }
            return conversation;
        }

        public async Task<bool> AddNewConversationAsync()
        {
            return await ConversationData.AddNewConversationAsync(new ConversationDto(this.ConversationId,this.CreatedAt));
        }
    }
}
