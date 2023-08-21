using Challenges.Domain.Entities.Common;

namespace Challenges.Domain.Entities;

public class NotificationDetail : Base
{
    private NotificationDetail()
    {
    }
    public NotificationDetail(NotificationType notificationType, string? fcmToken, bool isAndroidDevice, string? title, string? body)
    {
        NotificationTypeId = notificationType.Id;
        FcmToken = fcmToken;
        IsAndroidDevice = isAndroidDevice;
        Title = title;
        Body = body;
    }
    
    public string? FcmToken { get; set; }
    public bool IsAndroidDevice { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
    public Guid NotificationTypeId { get; set; }
    public NotificationType? NotificationType { get; set; }
}