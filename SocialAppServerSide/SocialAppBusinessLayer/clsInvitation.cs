using SocialAppDataLayer;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppBusinessLayer
{
    public class clsInvitation
    {
        public string InvitationId { get; set; } = null!;

        public string SenderId { get; set; } = null!;

        public string RecieverId { get; set; } = null!;

        public short InvitationStatus { get; set; }

        public DateTime SentAt { get; set; }

        // public InvitationStatus InvitationStatusNavigation { get; set; } = null!;

        public clsUser Reciever { get; set; } = null!;

        public clsUser Sender { get; set; } = null!;

        public InvitationDto Invitationdto 
        {
            get 
            {
                return new InvitationDto(this.InvitationId,this.SenderId,this.RecieverId,this.InvitationStatus,this.SentAt);
            }
        }

        public clsInvitation()
        {
            
        }

        public clsInvitation(InvitationDto Invitation)
        {
            this.InvitationId = Invitation.InvitationId;
            this.SenderId = Invitation.SenderId;
            this.RecieverId = Invitation.RecieverId;
            this.InvitationStatus = Invitation.InvitationStatus;
            this.SentAt = Invitation.SentAt;
        }

        public static async Task<clsInvitation>? GetInvitationByIdAsync(string InvitationId)
        {
            InvitationDto invitationDto = await InvitationData.GetInvitaionByIdAsync(InvitationId);
            clsInvitation? invitation = null;
            if (invitationDto != null)
            {
                invitation = MapperConfigBusiness.Mapper.Map<clsInvitation>(invitationDto);
            }
            return invitation;
        }

        public  async Task<bool> AddNewInvitationAsync()
        {
            return await InvitationData.AddNewInvitationAsync(new InvitationDto(this.InvitationId,this.SenderId,this.RecieverId,
                this.InvitationStatus,this.SentAt));
        }

        public static async Task<List<InvitationDto>> GetAllPendingInvitationByUserIdAsync(string UserId)
        {
            return await InvitationData.GetAllPendingInvitationByUserIdAsync(UserId);
        }

        public  async Task<bool> ChangeInvitationStatusAsync()
        {
            return await InvitationData.ChangeInvitationStatusAsync(this.InvitationId,this.InvitationStatus);
        }

        public static async Task<InvitationDto> IsInvitationExistsAsync(string SenderId, string RecieverId)
        {
            return await InvitationData.IsInvitationExistsAsync(SenderId,RecieverId);
        }

        public static async Task<bool> DeleteInvitationASync(string SenderId, string RecieverId)
        {
            return await InvitationData.DeleteInvitationASync(SenderId,RecieverId);
        }

    }
}
