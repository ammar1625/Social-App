namespace SocialAppApi.Models
{
    public class UserResponseDto
    {
        public string UserId { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = null!;

       // public string PassWord { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? ProfilePic { get; set; }

        public bool IsEmailVerified { get; set; }

        public bool IsActive { get; set; }

        public char Gender { get; set; }

        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null;
    }
}
