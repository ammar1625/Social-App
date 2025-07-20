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
    public class clsNotification
    {
        public string NotificationId { get; set; } = null!;

        public string? Content { get; set; }

        public string PostId { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public short NotificationTypeId { get; set; }

        public DateTime NotificationDate { get; set; }

        public bool IsRead { get; set; }

        public clsNotificationType NotificationType { get; set; } = null!;

        public clsPost Post { get; set; } = null!;

        public clsUser User { get; set; } = null!;

        public NotificationDto NotificationDto
        {
            get
            {
                return new NotificationDto(this.NotificationId,this.Content,this.PostId,this.UserId,this.NotificationTypeId,
                    this.NotificationDate,this.IsRead);
            }
        }

        public clsNotification()
        {
            
        }

        public clsNotification(NotificationDto Notification)
        {
            this.NotificationId = Notification.NotificationId;
            this.Content = Notification.Content;
            this.PostId = Notification.PostId;
            this.UserId = Notification.UserId;
            this.NotificationTypeId = Notification.NotificationTypeId;
            this.NotificationDate = Notification.NotificationDate;
            this.IsRead = Notification.IsRead;
        }

        public static async Task<clsNotification>? GetNotificationByIdAsync(string Notificationid)
        {
            NotificationDto? notificationDto = await NotificationData.GetNotificationByIdAsync(Notificationid);
            clsNotification? notification = null;
            if (notificationDto != null)
            {
                notification = MapperConfigBusiness.Mapper.Map<clsNotification>(notificationDto);
            }

            return notification;
        }

        public async Task<bool> AddNewNotificationAsync()
        {
            return await NotificationData.AddNewNotificationAsync(new NotificationDto(this.NotificationId,this.Content,
                this.PostId,this.UserId,this.NotificationTypeId,this.NotificationDate,this.IsRead));
        }

        public static async Task<List<DetailedNotificationDto>>? GetAllUnreadNotificationsAsync(string UserId)
        {
            return await NotificationData.GetAllUnreadNotificationsAsync(UserId);
        }

        public static async Task<int> GetUnreadNotificationsCountAsync(string UserId)
        {
            return await NotificationData.GetUnreadNotificationsCountAsync(UserId);
        }

        public async Task<bool> ChangeNotificationStatusAsync()
        {
            return await NotificationData.ChangeNotificationStatusAsync(this.NotificationId);
        }
    }
}
