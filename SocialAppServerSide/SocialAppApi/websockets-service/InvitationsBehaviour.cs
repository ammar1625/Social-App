using SocialAppApi.Models;
using SocialAppBusinessLayer;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebSocketSharp;
using WebSocketSharp.Server;
using SocialAppDataLayer.Dtos;

namespace SocialAppApi.websockets_service
{
    public class InvitationsBehaviour:WebSocketBehavior
    {
        public InvitationsBehaviour()
        {
            
        }

        protected override async void OnMessage(MessageEventArgs e)
        {
            var DeserializationOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var SerializationOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };


            try
            {

                InvitationModel? Invitation = JsonSerializer.Deserialize<InvitationModel>(e.Data, DeserializationOptions);

                clsInvitation NewInvitation = new clsInvitation(new InvitationDto(
                  Guid.NewGuid().ToString(),
                  Invitation.SenderId,
                  Invitation.RecieverId,
                  1,
                  DateTime.Now
                 ));

                switch (Invitation.Type)
                {
                    case 1:
                     
                        //handle new pending invitation case
                        await NewInvitation.AddNewInvitationAsync();

                        NewInvitation.Sender = await clsUser.GetUserByIdAsync(NewInvitation.SenderId);

                        var SerializedInvitation = JsonSerializer.Serialize(NewInvitation, SerializationOptions);

                        Sessions.Broadcast(SerializedInvitation);
                        break;

                    case 2:
                         //hande accepting invitation case
                         NewInvitation.InvitationStatus = 2;
                         SerializedInvitation = JsonSerializer.Serialize(NewInvitation, SerializationOptions);

                         Sessions.Broadcast(SerializedInvitation);
                         break;
                    case 3:

                        //handle reject invitation case
                        NewInvitation.InvitationStatus = 3;
                        SerializedInvitation = JsonSerializer.Serialize(NewInvitation, SerializationOptions);

                        Sessions.Broadcast(SerializedInvitation);
                        break;

                    case 4:

                        //handle cancel invitation case
                        NewInvitation.InvitationStatus = 4;
                        SerializedInvitation = JsonSerializer.Serialize(NewInvitation, SerializationOptions);

                        Sessions.Broadcast(SerializedInvitation);
                        break;

                }
                
            }

            catch(Exception ex)
            {
               
            }


        }


    }
}
