using Radzen;

namespace VideoHolder.Shared.Notifications;

public class ErrorNotification: NotificationMessage
{
    public ErrorNotification(
        string summary,
        string detail
    )
    {
        Severity = NotificationSeverity.Error;
        Duration = 30000D;
        Summary = summary;
        Detail = detail;
    }
}