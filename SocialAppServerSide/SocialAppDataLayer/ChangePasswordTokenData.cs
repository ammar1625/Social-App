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
    public class ChangePasswordTokenData
    {
        public static async Task<bool> AddNewChangePasswordTokenAsync(ChangePassWordTokenDto token)
        {
            int AffectedRows = 0;
            using (AppDbContext context = new AppDbContext()) 
            {
                try
                {
                    await context.ChangePassWordTokens.AddAsync(MapperConfigData.Mapper.Map<ChangePassWordToken>(token));

                    AffectedRows = await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    
                }
               
            }

            return AffectedRows > 0;
        }

        public static async Task<ChangePassWordTokenDto>? GetValidTokenAsync(string Token)
        {
            ChangePassWordTokenDto tokendto = null;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    ChangePassWordToken? token = await Context.ChangePassWordTokens.FirstOrDefaultAsync(
                        t => t.ChangePassWordToken1 == Token
                        && t.ExpirationDate > DateTime.Now);

                    if (token != null)
                    {
                        tokendto = MapperConfigData.Mapper.Map<ChangePassWordTokenDto>(token);
                    }
                }
                catch (Exception e)
                {

                }
            }

            return tokendto;
        }
    }
}
