namespace SocialAppApi.Models
{
    public class FriendshipExistsModel
    {
        public string CurrentUserId { get; set; } = null!;
        public string TargetUserId { get; set; } = null!;
    }
}
