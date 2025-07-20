using SocialAppDataLayer;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Mapping;
using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer
{
    public class clsOtp
    {
        public string Id { get; set; } = null!;

        public int Code { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string UserId { get; set; } = null!;

        public bool IsUsed { get; set; }

        public clsUser User { get; set; } = null!;

        public OtpDto otpdto 
        {
            get {  return new OtpDto(this.Id , this.Code , this.CreatedAt ,this.ExpirationDate , this.UserId ,this.IsUsed); }
        }

        public clsOtp()
        {
            
        }
        public clsOtp(OtpDto otp)
        {
            this.Id = otp.Id;
            this.Code = otp.Code;
            this.CreatedAt = otp.CreatedAt;
            this.ExpirationDate = otp.ExpirationDate;
            this.UserId = otp.UserId;
            this.IsUsed = otp.IsUsed;
        }

        public async Task<bool> AddNewOtp()
        {
            return await OtpData.AddNewOtpAsync(new OtpDto(this.Id , this.Code , this.CreatedAt , this.ExpirationDate , 
                this.UserId , this.IsUsed));
        }

        public static async Task<bool>SetAllOtpUsedAsync(string UserId)
        {
            return await OtpData.SetAllOtpsUsedAsync(UserId);
        }

        public  async Task<bool> SetOptUsed(int Code)
        {
            return await OtpData.SetOtpUsedAsync(Code);
        }

        public static async Task<clsOtp>? GetOtpByCodeAsync(int Code)
        {
            OtpDto? otpdto = await OtpData.GetOtpByCodeAsync(Code);
            clsOtp otp = null;
            if(otpdto != null)
            {
                otp = MapperConfigBusiness.Mapper.Map<clsOtp>(otpdto);
            }

            return otp;
        }

       
    }
}
