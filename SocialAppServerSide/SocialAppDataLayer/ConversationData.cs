using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialAppDataLayer.Data;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Mapping;
using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer
{
    public class ConversationData
    {
        public static async Task<ConversationDto>? GetConversationByIdAsync(string ConverrsationId)
        {
            ConversationDto conversationDto = null;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Conversation? conversation = await Context.Conversations.FirstOrDefaultAsync(c=>c.ConversationId==ConverrsationId);
                    if(conversation != null)
                    {
                        conversationDto = MapperConfigData.Mapper.Map<ConversationDto>(conversation);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return conversationDto;
        }

        public static async Task<ConversationDto>? GetConversationByMembersAsync(string CurrentUserId ,string TargetUserId)
        {
            ConversationDto? conversationDto = null;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    List<Conversation> conversations = await Context.Conversations.FromSqlInterpolated($"EXEC SP_GetConversationByMembers @CurrentUserId = {CurrentUserId}, @TargetUserId = {TargetUserId}").ToListAsync();
                    if( conversations.Count>0 && conversations != null )
                    {
                        Conversation? conversation = conversations.FirstOrDefault();

                        conversationDto = MapperConfigData.Mapper.Map<ConversationDto>(conversation);

                    }
                }
                catch (Exception ex)
                {

                }
            }

            return conversationDto;
        }

        public static async Task<bool> AddNewConversationAsync(ConversationDto Conversation)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Conversation NewConversation = MapperConfigData.Mapper.Map<Conversation>(Conversation);
                    await Context.Conversations.AddAsync(NewConversation);
                    AffectedRows = await Context.SaveChangesAsync();
                }   
                catch (Exception ex)
                {

                }
            }

            return AffectedRows > 0;
        }
    }
}
