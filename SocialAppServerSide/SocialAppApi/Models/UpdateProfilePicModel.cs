namespace SocialAppApi.Models
{
    public class UpdateProfilePicModel
    {
        public string UserId { get; set; } = null!;
        public IFormFile ProfilePic { get; set; }
    }
}
