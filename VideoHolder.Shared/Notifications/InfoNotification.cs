using Radzen;

namespace VideoHolder.Shared.Notifications;

public class InfoNotification: NotificationMessage
{
    public InfoNotification(
        string summary,
        string detail
    )
    {
        Severity = NotificationSeverity.Info;
        Duration = 5000D;
        Summary = summary;
        Detail = detail;
    }
}