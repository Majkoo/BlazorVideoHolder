using Radzen;

namespace VideoHolder.Shared.Notifications;

public class WarningNotification: NotificationMessage
{
    public WarningNotification(
        string summary,
        string detail
    )
    {
        Severity = NotificationSeverity.Warning;
        Duration = 10000D;
        Summary = summary;
        Detail = detail;
    }
}