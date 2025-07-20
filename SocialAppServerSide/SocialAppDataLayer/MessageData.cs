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
    public class MessageData
    {
        public static async Task<bool> AddNewMessageAsync(MessageDto message)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Message Message = MapperConfigData.Mapper.Map<Message>(message);

                    await Context.Messages.AddAsync(Message);

                    AffectedRows = await Context.SaveChangesAsync();

                }
                catch (Exception ex)
                {

                    
                }
              
            }

            return AffectedRows > 0;
        }

        public static async Task<List<MessageDto>> GetAllMessagesByConversationIdAsync(string ConversationId)
        {
            List<MessageDto> Messagesdto = new List<MessageDto>();
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    List<Message> Messages = await Context.Messages
                        .Include(m=>m.Sender)
                        .Where(m=>m.ConversationId==ConversationId)
                        .ToListAsync();

                    if(Messages.Count>0)
                    {
                        Messagesdto = MapperConfigData.Mapper.Map<List<MessageDto>>(Messages);
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return Messagesdto;
        }
    }
}
