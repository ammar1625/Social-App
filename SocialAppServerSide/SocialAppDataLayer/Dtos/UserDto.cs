using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class UserDto
    {
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


        // public ICollection<PostDto> Posts { get; set; }

        public UserDto()
        {
            
        }

        public UserDto(string UserId , string FirstName , string LastName , string UserName, DateTime DateOfBirth , string Email,
            string PassWord , string Phone , string? ProfilePic , bool IsEmailVerified , bool IsActive , char Gender)
        {
            this.UserId = UserId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.UserName = UserName;
            this.DateOfBirth = DateOfBirth;
            this.Email = Email;
            this.PassWord = PassWord;
            this.Phone = Phone;
            this.ProfilePic = ProfilePic;
            this.IsEmailVerified = IsEmailVerified;
            this.IsActive = IsActive;
            this.Gender = Gender;
        }
    }
}
