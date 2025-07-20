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
    public class RefreshTokenData
    {
        public static async Task<bool> AddNewRefreshTokenAsync(RefreshTokenDto token)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    RefreshToken RefreshToken = MapperConfigData.Mapper.Map<RefreshToken>(token);

                    await Context.RefreshTokens.AddAsync(RefreshToken);

                    AffectedRows = await Context.SaveChangesAsync();
                }
                catch (Exception ex) 
                {
                }
              
            }

            return (AffectedRows > 0);
        }

        public static async Task<bool> DeleteAllRefreshTokenByUserIdAsync(string UserId)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    List<RefreshToken> RefreshTokens = await Context.RefreshTokens.Where(rt => rt.UserId == UserId).ToListAsync();

                    if(RefreshTokens.Count > 0)
                    {
                        Context.RefreshTokens.RemoveRange(RefreshTokens);

                        AffectedRows = await Context.SaveChangesAsync();
                    }
                }
                catch (Exception ex) 
                {

                }
            }

            return AffectedRows > 0;
        }

        public static async Task<RefreshTokenDto>? GetValidRefreshTokenAsync(string Token)
        {
            RefreshTokenDto? reftokendto = null;
            using(AppDbContext Context = new AppDbContext())
            {
                try
                {
                    RefreshToken? reftoken = await Context.RefreshTokens.FirstOrDefaultAsync(r=>r.RefreshToken1==Token&&
                    r.ExpirationDate>DateTime.Now);

                    if(reftoken != null)
                    {
                        reftokendto = MapperConfigData.Mapper.Map<RefreshTokenDto>(reftoken);
                    }
                }
                catch (Exception ex) 
                {
                }
            }

            return reftokendto;
        }
    }
}
