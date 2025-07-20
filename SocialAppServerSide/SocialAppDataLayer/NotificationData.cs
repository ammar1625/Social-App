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
    public class NotificationData
    {
        public static async Task<NotificationDto>? GetNotificationByIdAsync(string NotificationId)
        {
            NotificationDto? notificationDto = null;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Notification? Notification = await Context.Notifications
                        .FirstOrDefaultAsync(n => n.NotificationId == NotificationId);

                    if (Notification != null)
                    {
                        notificationDto = MapperConfigData.Mapper.Map<NotificationDto>(Notification);
                    }
                }
                catch (Exception ex)
                {


                }
            }

            return notificationDto;
        }

        public static async Task<bool> AddNewNotificationAsync(NotificationDto NewNotification)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Notification Notification = MapperConfigData.Mapper.Map<Notification>(NewNotification);

                    Context.Notifications.Add(Notification);

                    AffectedRows = await Context.SaveChangesAsync();

                }
                catch (Exception ex)
                {


                }
            }

            return AffectedRows > 0;
        }

        public static async Task<List<DetailedNotificationDto>> GetAllUnreadNotificationsAsync(string UserId)
        {
            List<DetailedNotificationDto> NotificationsDto = new List<DetailedNotificationDto>();
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    List<DetailedNotification> Notifications = await Context.NotificationsList
                        .FromSqlInterpolated($"Exec Sp_GetNotificationsByUserId @UserId = {UserId}").ToListAsync();

                    if (Notifications.Count > 0)
                    {
                        NotificationsDto = MapperConfigData.Mapper.Map<List<DetailedNotificationDto>>(Notifications);
                    }
                }
                catch (Exception ex)
                {


                }
            }

            return NotificationsDto;
        }

        public static async Task<int> GetUnreadNotificationsCountAsync(string UserId)
        {
            int UnreadNotificationsCount = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    var list = await Context.UnreadNotificationsCount
                        .FromSqlInterpolated($"Exec Sp_GetCountOfUnreadNotificationsByUserId @UserId = {UserId}").ToListAsync();

                    if (list.Count > 0)
                        UnreadNotificationsCount = list.FirstOrDefault().UnreadNotificationCount;
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            return UnreadNotificationsCount;
        }

        public static async Task<bool> ChangeNotificationStatusAsync(string NotificationId)
        {
            int AffectedRows = 0;
            using (AppDbContext Context = new AppDbContext())
            {
                try
                {
                    Notification? NotificationTochange = await Context.Notifications
                        .FirstOrDefaultAsync(n => n.NotificationId == NotificationId);

                    if (NotificationTochange != null)
                    {
                        NotificationTochange.IsRead = true;
                        AffectedRows = await Context.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {

                }

                return AffectedRows > 0;
            }


        }
    }
}
