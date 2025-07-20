namespace SocialAppApi.Models
{
    public class ResetPasswordModel
    {
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
