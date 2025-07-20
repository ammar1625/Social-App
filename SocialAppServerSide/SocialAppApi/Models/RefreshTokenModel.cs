namespace SocialAppApi.Models
{
    public class RefreshTokenModel
    {
        public string UserId { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
