using SocialAppApi.Models;
using SocialAppBusinessLayer;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;
using SocialAppDataLayer.Dtos;

namespace SocialAppApi.websockets_service
{
  

    public class NotificationsBehavior : WebSocketBehavior
    {
        public NotificationsBehavior()
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
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };


            try
            {
                NewNotificationModel? Notification = JsonSerializer.Deserialize<NewNotificationModel>(e.Data, DeserializationOptions);

                clsNotification NewNotification = new clsNotification(
                    new NotificationDto(Guid.NewGuid().ToString(), Notification.Content, Notification.PostId, Notification.UserId, Notification.NotificationTypeId, DateTime.Now, false)
                    );

                await NewNotification.AddNewNotificationAsync();
                NewNotification.User = await clsUser.GetUserByIdAsync(NewNotification.UserId);
                NewNotification.Post = await clsPost.GetPostByidAsync(NewNotification.PostId);
                var NotificationObj = new { NewNotification.NotificationId, NewNotification.Content, NewNotification.PostId, NewNotification.UserId, NewNotification.NotificationTypeId, NewNotification.NotificationDate, NewNotification.IsRead, NewNotification.User.FirstName, NewNotification.User.LastName, NewNotification.User.ProfilePic, NewNotification.User.Gender , NotificationRecieverId = NewNotification.Post.UserId };
                string SerializedNotification = JsonSerializer.Serialize(NotificationObj, SerializationOptions);
                Sessions.Broadcast(SerializedNotification);

            }
            catch (Exception ex)
            {
            }

        }

        protected override void OnOpen()
        {
            Console.WriteLine("Client connected.");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine("Client disconnected.");
        }
    }

}
