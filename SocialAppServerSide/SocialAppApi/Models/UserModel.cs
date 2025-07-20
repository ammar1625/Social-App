namespace SocialAppApi.Models
{
    public class UserModel
    {
        //public string UserId { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = null!;

        public string PassWord { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public bool IsEmailVerified { get; set; }

        public bool IsActive { get; set; }

        public char Gender { get; set; }

        public UserModel()
        {
            
        }
    }
}
