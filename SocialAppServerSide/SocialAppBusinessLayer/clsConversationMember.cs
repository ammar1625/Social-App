
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
    public class clsConversationMember
    {
        public string ConversationMemberId { get; set; } = null!;

        public string ConversationId { get; set; } = null!;

        public string UserId { get; set; } = null!;

        //public Conversation Conversation { get; set; } = null!;

        public clsUser User { get; set; } = null!;

        public ConversationMemberDto ConversationMemberdto 
        { 
            get 
            {
                return new ConversationMemberDto(this.ConversationMemberId,this.ConversationId,this.UserId);
            }
        }

        public clsConversationMember()
        {
            
        }

        public clsConversationMember(ConversationMemberDto Conversationmember)
        {
            this.ConversationMemberId = Conversationmember.ConversationMemberId;
            this.ConversationId = Conversationmember.ConversationId;
            this.UserId = Conversationmember.UserId;
        }

        public static async Task<clsConversationMember>? GetConversationMemberByIdAsync(string ConversationMemberId)
        {
            ConversationMemberDto? ConversationMemberdto = await ConversationMemberData
                .GetConversationMemberByIdAsync(ConversationMemberId);
            clsConversationMember ConversationMember = null;
            if(ConversationMemberdto != null)
            {
                ConversationMember = MapperConfigBusiness.Mapper.Map<clsConversationMember>(ConversationMemberdto);
                //ConversationMember.ConversationMemberdto.User = MapperConfigBusiness.Mapper.Map<UserDto>(ConversationMember.User);
            }

            return ConversationMember;
        }

        public  async Task<bool> AddNewConversationMemberAsync()
        {
            return await ConversationMemberData.AddNewConversationMemberAsync(new ConversationMemberDto(this.ConversationMemberId,
                this.ConversationId,this.UserId));
        }
        public static async Task<List<DetailedConversationMemberDto>> GetConversationsListByUserIdAsync(string UserId)
        {
            return await ConversationMemberData.GetConversationsListByUserIdAsync(UserId);
        }
    }
}
