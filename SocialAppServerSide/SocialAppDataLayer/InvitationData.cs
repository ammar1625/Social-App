using Microsoft.EntityFrameworkCore;
using SocialAppDataLayer.Data;
using SocialAppDataLayer.Dtos;
using SocialAppDataLayer.Mapping;
using SocialAppDataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialAppDataLayer
{
    public class InvitationData
    {
        public static async Task<InvitationDto>? GetInvitaionByIdAsync(string InvitationId)
        {
            InvitationDto invitationDto = null;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Invitation? invitation = await Context.Invitations.FirstOrDefaultAsync(i=>i.InvitationId==InvitationId);
                    if (invitation != null) 
                    {
                        invitationDto = MapperConfigData.Mapper.Map<InvitationDto>(invitation);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return invitationDto;
        }

        public static async Task<bool> AddNewInvitationAsync(InvitationDto Invitation)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Invitation NewInvitation = MapperConfigData.Mapper.Map<Invitation>(Invitation);
                    await Context.Invitations.AddAsync(NewInvitation);

                    AffectedRows = await Context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                }
            }
            return AffectedRows > 0;
        }

        public static async Task<List<InvitationDto>> GetAllPendingInvitationByUserIdAsync(string UserId)
        {
            List<InvitationDto> InvitationsDtos = new List<InvitationDto>();
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    //List<Invitation> Invitations = await Context.Invitations
                    //    .FromSqlInterpolated($"Exec SP_GetAllPendingInvitations @UserId = {UserId}").ToListAsync();

                    List<Invitation> Invitations = await Context.Invitations
                        .Include(i=>i.Sender)
                        .Include(i=>i.Reciever)
                        .Where(i=>i.RecieverId==UserId && i.InvitationStatus==1
                        ).ToListAsync();

                    if(Invitations.Count>0)
                    {
                        InvitationsDtos = MapperConfigData.Mapper.Map<List<InvitationDto>>(Invitations);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return InvitationsDtos;
        }

        public static async Task<bool> ChangeInvitationStatusAsync(string InvitationId , short Status)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Invitation? Invitation = await Context.Invitations.FirstOrDefaultAsync(i=>i.InvitationId==InvitationId);

                    if(Invitation != null)
                    {
                        Invitation.InvitationStatus = Status;

                        AffectedRows = await Context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return AffectedRows > 0;
        }

        public static async Task<InvitationDto>? IsInvitationExistsAsync(string SenderId, string RecieverId)
        {
            InvitationDto? Invitationdto = null;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Invitation? invitation = await Context.Invitations
                        .FirstOrDefaultAsync(i=>i.SenderId==SenderId&&i.RecieverId==RecieverId&&i.InvitationStatus==1);

                    if(invitation != null)
                    {
                        Invitationdto = MapperConfigData.Mapper.Map<InvitationDto>(invitation);
                    }

                }
                catch (Exception ex)
                {

                }
            }

            return Invitationdto;
        }

        public static async Task<bool> DeleteInvitationASync(string SenderId , string RecieverId)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Invitation invitation = await Context.Invitations
                        .FirstOrDefaultAsync(i => i.SenderId == SenderId && i.RecieverId == RecieverId && i.InvitationStatus == 1);
                    if (invitation != null)
                    {
                        Context.Invitations.Remove(invitation);
                        AffectedRows = await Context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return (AffectedRows>0);
        }
    }
}
