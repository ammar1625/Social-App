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
    public class ConversationMemberData
    {
        public static async Task<ConversationMemberDto>? GetConversationMemberByIdAsync(string ConversationMemberId)
        {
            ConversationMemberDto conversationMemberDto = null;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    ConversationMember ConversationMember = await Context.ConversationMembers
                        .Include(c=>c.User)
                        .FirstOrDefaultAsync(c=>c.ConversationMemberId==ConversationMemberId);

                    if(ConversationMember != null)
                    {
                        conversationMemberDto = MapperConfigData.Mapper.Map<ConversationMemberDto>(ConversationMember);
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return conversationMemberDto;
        }

        public static async Task<bool> AddNewConversationMemberAsync(ConversationMemberDto ConversationMember)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    ConversationMember NewConversationMember = MapperConfigData
                        .Mapper.Map<ConversationMember>(ConversationMember);

                    await Context.ConversationMembers.AddAsync(NewConversationMember);
                    AffectedRows = await Context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }
            return AffectedRows > 0;
        }

        public static async Task<List<DetailedConversationMemberDto>> GetConversationsListByUserIdAsync(string UserId)
        {
            List<DetailedConversationMemberDto> ConversationMembersdto = new();
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    List<DetailedConversationMember> ConversationMembers = await Context.ConversationsList
                        .FromSqlInterpolated($"Exec Sp_GetConversationsByUserId @UserId={UserId}").ToListAsync();

                    if(ConversationMembers.Count>0)
                    {
                        ConversationMembersdto = MapperConfigData.Mapper.Map<List<DetailedConversationMemberDto>>(ConversationMembers);
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return ConversationMembersdto;
        }
    }
}
