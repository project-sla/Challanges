using Challenges.Domain.Entities.Common;

namespace Challenges.Domain.Entities;

public class NotificationType : Base
{
    public NotificationType(string type)
    {
        Type = type;
    }

    public string Type { get; set; }
}