namespace SocialAppApi.Models
{
    public class InvitationModel
    {
        public short Type { get; set; }
        public string SenderId { get; set; } = null!;

        public string RecieverId { get; set; } = null!;

    
    }
}
