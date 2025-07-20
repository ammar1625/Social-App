namespace SocialAppApi.Models
{
    public class NewNotificationModel
    {
        public string? Content { get; set; }
        public string PostId { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public short NotificationTypeId { get; set; }
     
    }
}
