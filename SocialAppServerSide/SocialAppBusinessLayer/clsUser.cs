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
    public class clsUser
    {
        //public enum EnMode { AddNew=0 , Update=1 }

        //public EnMode Mode { get; set; }
        public string UserId { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        //public string FullName
        //{
        //    get
        //    {
        //        return this.FirstName + " " + this.LastName;
        //    }
        //}

        public string UserName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = null!;

        public string PassWord { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? ProfilePic { get; set; }

        public bool IsEmailVerified { get; set; }

        public bool IsActive { get; set; }

        public char Gender { get; set; }


        // public ICollection<clsPost> Posts { get; set; }

        public UserDto userdto 
        {
            get 
            {
                return new UserDto(this.UserId,this.FirstName,this.LastName,this.UserName,this.DateOfBirth,this.Email,
                    this.PassWord,this.Phone,this.ProfilePic,this.IsEmailVerified,this.IsActive,this.Gender);
            }
        }

        public clsUser()
        {
            
        }

        public clsUser(UserDto user /*, EnMode Mode = EnMode.Update*/)
        {
            this.UserId = user.UserId;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.UserName = user.UserName;
            this.DateOfBirth = user.DateOfBirth;
            this.Email = user.Email;
            this.PassWord = user.PassWord;
            this.Phone = user.Phone;
            this.ProfilePic = user.ProfilePic;
            this.IsEmailVerified = user.IsEmailVerified;
            this.IsActive = user.IsActive;
            this.Gender = user.Gender;
            //this.Mode = Mode;
        }

        public static async Task<clsUser>? GetUserByIdAsync(string UserId)
        {
           UserDto user = await UserData.GetUserByIdAsync(UserId);

            if(user != null)
            {
                clsUser User = MapperConfigBusiness.Mapper.Map<clsUser>(user);
                return User;
            }

            return null;
        }

        public static async Task<clsUser>? GetUserByEmailAsync(string Email)
        {
            UserDto? User = await UserData.GetUserByEmailAsync(Email);

            if (User != null)
            {
                clsUser user = MapperConfigBusiness.Mapper.Map<clsUser>(User);
                return user;
            }

            return null;
        }

        public async Task<bool> VerifyEmailAsync()
        {
            return await UserData.VerifyEmailAsync(this.UserId);
        }

        public  async Task<bool> AddNewUserAsync()
        {
            this.UserId = await UserData.AddNewUserAsync(new UserDto(" ",this.FirstName,this.LastName , this.UserName,this.DateOfBirth,
                this.Email,this.PassWord,this.Phone,this.ProfilePic,this.IsEmailVerified, this.IsActive , this.Gender));

            return (this.UserId != null);
        }

        public static async Task<bool> LogInAsync(string Email, string PassWord)
        {
            return await UserData.LoginAsync(Email, PassWord);
        }

        public static async Task<clsUser>? TwoFaLoginAsync(int? Code)
        {
            //get the userdto from data layer
            UserDto? userdto = await UserData.TwoFaLoginAsync(Code);
            clsUser user = null;

            //if there is a user found then copy it in clsuser object and return it otherwise return null
            if (userdto != null)
            {
                user = MapperConfigBusiness.Mapper.Map<clsUser>(userdto);
            }

            return user;
        }

        public async Task<bool> ResetPassWordAsync()
        {
            return await UserData.ResetPassWordAsync(this.UserId , this.PassWord);
        }

        public static async Task<bool> DeleteUserAsync(string Email , string PassWord)
        {
            return await UserData.DeleteUserAsync(Email, PassWord);
        }

        public async Task<bool> UpdateUserCredentialsAsync()
        {
            return await UserData.UpdateUserCredentialsAsync(new UpdateUserDto(this.UserId , this.FirstName,this.LastName,
                this.UserName,this.DateOfBirth , this.Email , this.Phone ,this.Gender));
        }

        public async Task<bool> UpdateProfilePicAsync()
        {
            return await UserData.UpdateProfilePicAsync(this.UserId,this.ProfilePic);
        }

        public static async Task<bool> IsUserExistsByEmailAsync(string Email , string? UserId)
        {
            return await UserData.IsUserExistsByEmailAsync(Email, UserId);
        }

        public static async Task<bool> IsUserExistsByUserNameAsync(string UserName,string? UserId)
        {
            return await UserData.IsUserExistsByUserNameAsync(UserName, UserId);
        }

        public static async Task<List<UserDto>> GetUsersListAsync(string NameFilter)
        {
            return await UserData.GetUsersListAsync(NameFilter);
        }
    }
}
