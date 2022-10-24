using Radzen;

namespace VideoHolder.Shared.Notifications;

public class SuccessNotification: NotificationMessage
{
    public SuccessNotification(
        string summary,
        string detail
    )
    {
        Severity = NotificationSeverity.Success;
        Duration = 5000D;
        Summary = summary;
        Detail = detail;
    }
}