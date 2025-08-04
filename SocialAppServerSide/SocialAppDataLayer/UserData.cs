using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class UserData
    {
        public static async Task<UserDto>? GetUserByIdAsync(string UserId)
        {
            UserDto Userdto = new UserDto();
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    User? user = await Context.Users.FirstOrDefaultAsync(u=>u.UserId == UserId);

                    if (user == null)
                    {
                        return null;
                    }

                    Userdto =  MapperConfigData.Mapper.Map<UserDto>(user);
                    
                }
                catch (Exception ex)
                {

                }
                
            }

            return  Userdto;

        }

        public static async Task<UserDto>? GetUserByEmailAsync(string Email)
        {
            UserDto user = null;
            using (AppDbContext Context = new AppDbContext())
            {
                User? User = await Context.Users.FirstOrDefaultAsync(u=>u.Email ==  Email);

                if (User == null)
                {
                    return null;
                }

                user = MapperConfigData.Mapper.Map<UserDto>(User);
            }

            return user;
        }

        public async static Task<bool> VerifyEmailAsync(string UserId)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext()) 
            {
                User? UserToVerify = await Context.Users.FirstOrDefaultAsync(u=>u.UserId==UserId);

                if(UserToVerify != null)
                {
                    UserToVerify.IsEmailVerified = true;
                    AffectedRows = await Context.SaveChangesAsync();

                }

            }

            return (AffectedRows > 0);
        }

        public static async  Task<string>? AddNewUserAsync(UserDto user)
        {
            string? NewId = null;
            using (AppDbContext Context = new AppDbContext()) 
            {
                try
                {
                    var UserIdParam = new SqlParameter 
                    {
                        ParameterName = "@NewId",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 50,
                        Direction = System.Data.ParameterDirection.Output
                    };
                   await Context.Database.ExecuteSqlInterpolatedAsync($"Exec Sp_AddNewUser @FirstName = {user.FirstName},@LastName = {user.LastName},@UserName = {user.UserName},@DateOfBirth = {user.DateOfBirth},@Email  = {user.Email},@PassWord = {user.PassWord},@Phone = {user.Phone},@ProfilePic = {user.ProfilePic},@IsEmailVerified = {user.IsEmailVerified},@IsActive = {user.IsActive}  ,@Gender = {user.Gender},@NewId = {UserIdParam} output");

                    NewId = UserIdParam.Value.ToString();
                }
                catch (Exception ex) 
                {
                }

                return NewId;
            }
        }

        public static async Task<bool> LoginAsync(string Email)
        {
            bool IsFound = false;
            using (AppDbContext Context = new AppDbContext()) 
            {
                try
                {
                   IsFound = await Context.Users.AnyAsync(u=> u.Email == Email && u.IsActive == true);
                }
                catch (Exception ex)
                {

                }
            }

            return IsFound;
        }

        public static async Task<UserDto>? TwoFaLoginAsync(int? Code)
        {
            UserDto? User = null;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    List<User> users = await Context.Users.FromSqlInterpolated($"Exec SP_TWOFALOGIN @Code = {Code}").ToListAsync();

                    User? user = users.FirstOrDefault();

                    if (user != null)
                    {
                        User = MapperConfigData.Mapper.Map<UserDto>(user);
                    }
                }
                catch (Exception ex)
                {
                }
            }

            return User;
        }

        public static async Task<bool> ResetPassWordAsync(string UserId , string NewPassWord)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext()) 
            {
                try
                {
                    //User? user = await Context.Users.FirstOrDefaultAsync(u=>u.UserId==UserId);

                    //if (user != null)
                    //{
                    //    user.PassWord = NewPassWord;

                    //    AffectedRows = await Context.SaveChangesAsync();
                    //}

                    AffectedRows = await Context.Database.ExecuteSqlInterpolatedAsync($@"
                                                    Exec Sp_ChangePassWord 
                                                    @UserId={UserId},
                                                    @NewPassWord={NewPassWord}");
                }
                catch (Exception ex) 
                {
                }
            }
            return AffectedRows > 0;
        }

        public static async Task<bool> DeleteUserAsync(string Email , string PassWord)
        {
            int AffectedRows = 0;
            using(AppDbContext Context = new AppDbContext())
            {
                //User? UserToDelete = await Context.Users.FirstOrDefaultAsync(u=>u.Email==Email && u.PassWord==PassWord);
                
                //if(UserToDelete != null)
                //{
                //     Context.Users.Remove(UserToDelete);

                //     AffectedRows = await Context.SaveChangesAsync();
                //}

                AffectedRows = await Context.Database.ExecuteSqlInterpolatedAsync($@"
                                                       Exec Sp_DeleteUser
                                                       @Email={Email},
                                                       @PassWord={PassWord}");
            }
            return AffectedRows > 0;
        }

        public static async Task<bool> UpdateUserCredentialsAsync(UpdateUserDto UserModel)
        {
            int AffectedRows = 0;
            using(AppDbContext Context = new AppDbContext())
            {
                try
                {
                    //User? User = await Context.Users.FirstOrDefaultAsync(u => u.UserId == UserModel.UserId);
                    //if (User != null)
                    //{
                    //    User.FirstName = UserModel.FirstName;
                    //    User.LastName = UserModel.LastName;
                    //    User.Email = UserModel.Email;
                    //    User.DateOfBirth = UserModel.DateOfBirth;
                    //    User.UserName = UserModel.UserName;
                    //    User.Phone = UserModel.Phone;

                    //    AffectedRows = await Context.SaveChangesAsync();
                    //}

                    AffectedRows = await Context.Database.ExecuteSqlInterpolatedAsync($@"
                                   EXEC sp_updateuser 
                                       @UserId={UserModel.UserId}, 
                                       @FirstName={UserModel.FirstName}, 
                                       @LastName={UserModel.LastName},
                                       @UserName={UserModel.UserName},
                                       @DateOfBirth={UserModel.DateOfBirth},
                                       @Email={UserModel.Email},
                                       @Phone={UserModel.Phone},
                                       @Gender = {UserModel.Gender}
                               ");

                }
                catch (Exception ex)
                {
                }
            }

            return AffectedRows > 0;
        }

        public static async Task<bool> UpdateProfilePicAsync(string UserId,string ProfilePicUrl)
        {
            int AffectedRows = 0;
            using(AppDbContext Context = new AppDbContext())
            {
                try
                {
                    User? User = await Context.Users.FirstOrDefaultAsync(u=>u.UserId==UserId);

                    if (User != null) 
                    {
                        User.ProfilePic = ProfilePicUrl;

                        AffectedRows = await Context.SaveChangesAsync();
                    }
                }
                catch (Exception ex) 
                {
                }
            }

            return AffectedRows > 0;
        }

        public static async Task<bool> IsUserExistsByEmailAsync(string Email , string? UserId)
        {
            bool IsExists = false;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    if(UserId == null)
                    IsExists = await Context.Users.AnyAsync(u=>u.Email==Email);
                    else
                    IsExists = await Context.Users.AnyAsync(u => u.Email == Email && u.UserId != UserId);

                }
                catch (Exception ex)
                {

                }
            }
            return IsExists;
        }

        public static async Task<bool> IsUserExistsByUserNameAsync(string UserName , string? UserId)
        {
            bool IsExists = false;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    if(UserId == null)
                    IsExists = await Context.Users.AnyAsync(u => u.UserName == UserName);
                    else
                    IsExists = await Context.Users.AnyAsync(u => u.UserName == UserName && u.UserId != UserId);
                }
                catch (Exception ex)
                {

                }
            }
            return IsExists;
        }

        public static async Task<List<UserDto>>GetUsersListAsync(string NameFilter)
        {
            List<UserDto> UsersDto = new List<UserDto>();
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    List<User> Users = await Context.Users
                    .Where(u=>u.FirstName.Contains(NameFilter) ||u.LastName.Contains(NameFilter)||u.UserName.Contains(NameFilter) || (u.FirstName+" "+u.LastName).Contains(NameFilter))
                    .ToListAsync();

                    if(Users.Count>0)
                    {
                        UsersDto = MapperConfigData.Mapper.Map<List<UserDto>>(Users);
                    }

                }
                catch (Exception ex)
                {

                }
            }
            return UsersDto;
        }
    }
}
