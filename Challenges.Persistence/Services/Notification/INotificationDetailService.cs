using Challenges.Domain.Entities;

namespace Challenges.Persistence.Services.Notification;

public interface INotificationDetailService
{
    Task<NotificationDetail> AddNotificationDetail(NotificationDetail notificationDetail);
    Task<NotificationDetail?> RemoveNotificationDetail(Guid notificationDetailGuid);
    Task<NotificationDetail?> GetNotificationDetail(Guid notificationDetailGuid);
    Task<NotificationDetail> UpdateNotificationDetail(NotificationDetail notificationDetail);
}