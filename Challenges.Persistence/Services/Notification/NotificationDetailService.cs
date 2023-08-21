using Challenges.Domain.Entities;
using Challenges.Persistence.Context;

namespace Challenges.Persistence.Services.Notification;

public class NotificationDetailService : INotificationDetailService
{
    private readonly ChallengeDbContext _challengeDbContext;

    public NotificationDetailService(ChallengeDbContext challengeDbContext)
    {
        _challengeDbContext = challengeDbContext;
    }

    public async Task<NotificationDetail> AddNotificationDetail(NotificationDetail notificationDetail)
    {
        await _challengeDbContext.NotificationDetails.AddAsync(notificationDetail);
        await _challengeDbContext.SaveChangesAsync();
        return notificationDetail;
    }

    public async Task<NotificationDetail?> RemoveNotificationDetail(Guid notificationDetailGuid)
    {
        var notificationDetail = await _challengeDbContext.NotificationDetails.FindAsync(notificationDetailGuid);
        if (notificationDetail is null) return null;
        _challengeDbContext.NotificationDetails.Remove(notificationDetail);
        await _challengeDbContext.SaveChangesAsync();
        return notificationDetail;
    }

    public async Task<NotificationDetail?> GetNotificationDetail(Guid notificationDetailGuid)
    {
        return await _challengeDbContext.NotificationDetails.FindAsync(notificationDetailGuid);
    }

    public async Task<NotificationDetail> UpdateNotificationDetail(NotificationDetail notificationDetail)
    {
        _challengeDbContext.NotificationDetails.Update(notificationDetail);
        await _challengeDbContext.SaveChangesAsync();
        return notificationDetail;
    }
}