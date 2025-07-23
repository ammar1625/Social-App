namespace SocialAppApi.Models
{
    public class MessageModel
    {
        public string SenderId { get; set; } = null!;

        public string ConversationId { get; set; } = null!;

        public string? Content { get; set; } = null!;

        public string? MessageMediaUrl { get; set; } = null!;

    }
}
