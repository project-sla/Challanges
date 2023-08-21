using Challenges.Domain.Entities;

namespace Challenges.Persistence.Services.Notification;

public interface INotificationTypeService
{
    Task<NotificationType> AddNotificationType(NotificationType notificationType);
    Task<NotificationType?> RemoveNotificationType(Guid notificationTypeGuid);
    Task<NotificationType?> GetNotificationType(Guid notificationTypeGuid);
    Task<NotificationType?> GetNotificationType(string notificationTypeName);

}