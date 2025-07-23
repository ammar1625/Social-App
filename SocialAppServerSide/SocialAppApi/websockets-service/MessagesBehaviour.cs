using SocialAppApi.Models;
using SocialAppBusinessLayer;
using SocialAppDataLayer.Dtos;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace SocialAppApi.websockets_service
{
    public class MessagesBehaviour:WebSocketBehavior
    {
        public MessagesBehaviour()
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
                MessageModel? DeserializedMessage = JsonSerializer.Deserialize<MessageModel>(e.Data , DeserializationOptions);

                clsMessage Message = new clsMessage(
                   new MessageDto(
                        Guid.NewGuid().ToString(),
                        DeserializedMessage.SenderId,
                        DeserializedMessage.ConversationId,
                        DeserializedMessage.Content,
                        DeserializedMessage.MessageMediaUrl,
                        DateTime.Now
                              )
                                                   );

                await Message.AddNewMessageAsync();

                string SerializedMessage = JsonSerializer.Serialize( e.Data , SerializationOptions );

                Sessions.Broadcast(SerializedMessage);

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }
}
