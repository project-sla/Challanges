namespace Challenges.Persistence.Services.Notification;

public interface INotificationService
{
    Task<Domain.Entities.Notification> AddNotification(Domain.Entities.Notification notification);
    Task<Domain.Entities.Notification?> RemoveNotification(Guid accountGuid, Guid notificationGuid);
    Task<List<Domain.Entities.Notification>?> GetNotifications(Guid accountGuid);
    Task<List<Domain.Entities.Notification>?> GetNotifications(Guid accountGuid, bool isRead);
    Task<Domain.Entities.Notification?> GetNotification(Guid accountGuid, Guid notificationGuid);
    Task<Domain.Entities.Notification> UpdateNotification(Domain.Entities.Notification notification);
    Task<Domain.Entities.Notification?> GetNotification(Guid notificationGuid);
    Task<List<Domain.Entities.Notification>?> GetNotifications(Guid accountGuid, int skip, int take);
    Task<Domain.Entities.Notification> SendNotification(Domain.Entities.Notification notification);
}