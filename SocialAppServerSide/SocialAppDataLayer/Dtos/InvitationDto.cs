using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer.Dtos
{
    public class InvitationDto
    {
        public string InvitationId { get; set; } = null!;

        public string SenderId { get; set; } = null!;

        public string RecieverId { get; set; } = null!;

        public short InvitationStatus { get; set; }

        public DateTime SentAt { get; set; }

       // public InvitationStatus InvitationStatusNavigation { get; set; } = null!;

        public UserDto Reciever { get; set; } = null!;

        public UserDto Sender { get; set; } = null!;

        public InvitationDto()
        {
            
        }

        public InvitationDto(string InvitationId , string SenderId , string RecieverId , short InvitationStatus,DateTime SentAt
            )
        {
            this.InvitationId = InvitationId;
            this.SenderId = SenderId;
            this.RecieverId = RecieverId;
            this.InvitationStatus = InvitationStatus;
            this.SentAt = SentAt;
        }
    }
}
