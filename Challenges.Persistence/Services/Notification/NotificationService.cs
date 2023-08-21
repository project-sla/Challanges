using System.Net.Http.Headers;
using Challenges.Infrastructure.Settings;
using Challenges.Persistence.Context;
using CorePush.Firebase;
using Microsoft.EntityFrameworkCore;

namespace Challenges.Persistence.Services.Notification;

public class NotificationService : INotificationService
{
    private readonly ChallengeDbContext _challengeDbContext;
    private readonly FcmNotificationSetting _fcmNotificationSetting;


    public NotificationService(FcmNotificationSetting fcmNotificationSetting, ChallengeDbContext challengeDbContext)
    {
        _fcmNotificationSetting = fcmNotificationSetting;
        _challengeDbContext = challengeDbContext;
    }


    public async Task<Domain.Entities.Notification> AddNotification(Domain.Entities.Notification notification)
    {
        await _challengeDbContext.Notifications.AddAsync(notification);
        await _challengeDbContext.SaveChangesAsync();
        return notification;
    }

    public async Task<Domain.Entities.Notification?> RemoveNotification(Guid accountGuid, Guid notificationGuid)
    {
        var notification = await _challengeDbContext.Notifications.FindAsync(notificationGuid);
        if (notification is null) return null;
        _challengeDbContext.Notifications.Remove(notification);
        await _challengeDbContext.SaveChangesAsync();
        return notification;
    }

    public async Task<List<Domain.Entities.Notification>?> GetNotifications(Guid accountGuid)
    {
        return await _challengeDbContext.Notifications.Where(e => e.ReceivedBy == accountGuid).ToListAsync();
    }

    public async Task<List<Domain.Entities.Notification>?> GetNotifications(Guid accountGuid, bool isRead)
    {
        return await _challengeDbContext.Notifications.Where(e => e.ReceivedBy == accountGuid && e.IsSent == isRead).ToListAsync();
    }

    public async Task<Domain.Entities.Notification?> GetNotification(Guid accountGuid, Guid notificationGuid)
    {
        return await _challengeDbContext.Notifications.FindAsync(notificationGuid);
    }

    public async Task<Domain.Entities.Notification> UpdateNotification(Domain.Entities.Notification notification)
    {
        _challengeDbContext.Notifications.Update(notification);
        await _challengeDbContext.SaveChangesAsync();
        return notification;
    }

    public async Task<Domain.Entities.Notification?> GetNotification(Guid notificationGuid)
    {
        return await _challengeDbContext.Notifications.FindAsync(notificationGuid);
    }

    public async Task<List<Domain.Entities.Notification>?> GetNotifications(Guid accountGuid, int skip, int take)
    {
        return await _challengeDbContext.Notifications.Where(e => e.ReceivedBy == accountGuid).Skip(skip).Take(take).ToListAsync();
    }

    public async Task<Domain.Entities.Notification> SendNotification(Domain.Entities.Notification notification)
    {
        var firebaseSettingsJson = await File.ReadAllTextAsync("/root/ChallengesCore/Challanges/Challenges.API/solvify-1b4d6-firebase-adminsdk-mpx0g-f73d40077c.json");
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("key", _fcmNotificationSetting.ServerKey);

        var googleNotification = new GoogleNotification
        {
            Message = new Message
            {
                Token = notification.NotificationDetail.FcmToken,
                Notification = new Message.NotificationPayload
                {
                    Title = notification.NotificationDetail.Title,
                    Body = notification.NotificationDetail.Body
                }
            }
        };

        var fcm = new FirebaseSender(firebaseSettingsJson, httpClient);
        await fcm.SendAsync(googleNotification);

        return notification;
    }
}