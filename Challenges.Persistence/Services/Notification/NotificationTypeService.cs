using Challenges.Domain.Entities;
using Challenges.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Persistence.Services.Notification;

public class NotificationTypeService : INotificationTypeService
{
    private readonly ChallengeDbContext _challengeDbContext;

    public NotificationTypeService(ChallengeDbContext challengeDbContext)
    {
        _challengeDbContext = challengeDbContext;
    }


    public async Task<NotificationType> AddNotificationType(NotificationType notificationType)
    {
        await _challengeDbContext.NotificationTypes.AddAsync(notificationType);
        await _challengeDbContext.SaveChangesAsync();
        return notificationType;
    }

    public async Task<NotificationType?> RemoveNotificationType(Guid notificationTypeGuid)
    {
        var notificationType = await _challengeDbContext.NotificationTypes.FindAsync(notificationTypeGuid);
        if (notificationType is null) return null;
        _challengeDbContext.NotificationTypes.Remove(notificationType);
        await _challengeDbContext.SaveChangesAsync();
        return notificationType;
    }

    public async Task<NotificationType?> GetNotificationType(Guid notificationTypeGuid)
    {
        return await _challengeDbContext.NotificationTypes.FindAsync(notificationTypeGuid);
    }

    public async Task<NotificationType?> GetNotificationType(string notificationTypeName)
    {
        return await _challengeDbContext.NotificationTypes.FirstOrDefaultAsync(e => e.Type == notificationTypeName);
    }
}