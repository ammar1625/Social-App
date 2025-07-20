namespace SocialAppApi.Models
{
    public class CommentModel
    {
        public string Content { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string PostId { get; set; } = null!;

    }
}
