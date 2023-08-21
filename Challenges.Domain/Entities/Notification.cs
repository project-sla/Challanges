using Challenges.Domain.Entities.Common;

namespace Challenges.Domain.Entities;

public class Notification : Base
{
    private Notification()
    {
    }

    public Notification(Guid receivedBy, bool isSent, bool isActive, NotificationDetail notificationDetail)
    {
        ReceivedBy = receivedBy;
        IsSent = isSent;
        IsActive = isActive;
        NotificationDetailId = notificationDetail.Id;
    }

    public Guid ReceivedBy { get; set; }
    public bool IsSent { get; set; }
    public bool IsActive { get; set; }
    public Guid NotificationDetailId { get; set; }
    public NotificationDetail? NotificationDetail { get; set; }
}