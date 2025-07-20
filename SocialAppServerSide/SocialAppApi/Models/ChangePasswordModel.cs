namespace SocialAppApi.Models
{
    public class ChangePasswordModel
    {
        public string Email { get; set; } = null!;
        public string NewPassWord { get; set; } = null!;

    }
}
