using Newtonsoft.Json;

namespace Challenges.Infrastructure.Settings;

public class NotificationModel
{
    [JsonProperty("fcmToken")] public string FcmToken { get; set; }

    [JsonProperty("isAndroidDevice")] public bool IsAndroidDevice { get; set; }

    [JsonProperty("title")] public string Title { get; set; }

    [JsonProperty("body")] public string Body { get; set; }
}

public class GoogleNotification
{
    [JsonProperty("message")] public Message Message { get; set; }
}

public class Message
{
    [JsonProperty("token")] public string Token { get; set; }

    [JsonProperty("notification")] public NotificationPayload Notification { get; set; }

    public class NotificationPayload
    {
        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("body")] public string Body { get; set; }
    }
}