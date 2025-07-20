namespace SocialAppApi.Models
{
    public class AddPostModel
    {
        
        public string? Content { get; set; }
        public IFormFile? Media { get; set; }
        public string UserId { get; set; } = null!;
    }
}
