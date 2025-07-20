using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class UpdateUserDto
    {
        public string UserId { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public UpdateUserDto()
        {
            
        }

        public UpdateUserDto(string UserId,string FirstName , string LastName , string UserName ,DateTime DateOfBirth,
            string Email , string Phone)
        {
            this.UserId = UserId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.UserName = UserName;
            this.DateOfBirth = DateOfBirth;
            this.Email = Email;
            this.Phone = Phone;
        }

    }
}
