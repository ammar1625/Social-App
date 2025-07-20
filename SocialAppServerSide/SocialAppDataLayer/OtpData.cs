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
    public class OtpData
    {
        public static async Task<bool> AddNewOtpAsync(OtpDto otp)
        {
            int AffectedRows = 0;
            using (AppDbContext context = new AppDbContext()) 
            {
                try
                {
                    Otp NewOtp = MapperConfigData.Mapper.Map<Otp>(otp);
                    await context.Otps.AddAsync(NewOtp);

                    AffectedRows = await context.SaveChangesAsync();

                }
                catch (Exception ex) 
                {
                }
            }

            return AffectedRows > 0;
        }

        public static async Task<bool> SetAllOtpsUsedAsync(string UserId)
        {
            int AffectedRows = 0;
            using (AppDbContext context = new AppDbContext()) 
            {
                List<Otp> Otps = context.Otps.Where(o=>o.UserId==UserId && !o.IsUsed).ToList();

                if(Otps.Count>0)
                {
                    foreach(Otp otp in Otps)
                    {
                        otp.IsUsed = true;
                    }

                    AffectedRows = await context.SaveChangesAsync();
                }
            }

            return (AffectedRows > 0);
        }

        public static async Task<bool> SetOtpUsedAsync(int Code)
        {
            int AffectedRows = 0;
            using(AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Otp? otp = await Context.Otps.FirstOrDefaultAsync(o => o.Code == Code && !o.IsUsed && o.ExpirationDate>DateTime.Now);

                    if (otp != null)
                    {
                        otp.IsUsed = true;
                    }

                    AffectedRows = await Context.SaveChangesAsync();
                }
                catch (Exception ex) 
                {

                }
               
            }

            return (AffectedRows > 0);
        }

        public static async Task<OtpDto>? GetOtpByCodeAsync(int Code)
        {
            OtpDto otpdto = null;
            using(AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Otp? otp = await Context.Otps.FirstOrDefaultAsync(o=>o.Code==Code && !o.IsUsed && o.ExpirationDate > DateTime.Now);

                    if(otp != null)
                    {
                        otpdto = MapperConfigData.Mapper.Map<OtpDto>(otp);
                    }
                }
                catch (Exception ex) 
                {
                }
            }

            return otpdto;
        }
    }
}
