using Radzen;

namespace VideoHolder.View.Services;

public static class DictionaryService
{
    public static IDictionary<string, NotificationMessage> NotificationMessages { get; set; }
        = new Dictionary<string, NotificationMessage>()
    {
        {
            "NotImplemented",
            new NotificationMessage()
            {
                Severity = NotificationSeverity.Error,
                Summary = "NotImplementedException",
                Detail = "Programista jeszcze nie zaimplementowal tej funkcjonalności.",
                Duration = 5000
            }
        },
        {
            "SomethingWentWrong",
            new NotificationMessage()
            {
                Severity = NotificationSeverity.Error,
                Summary = "Niepowodzenie",
                Detail = "Coś poszło nie tak. Przepraszam za utrudnienia.",
                Duration = 5000
            }
        }
    };

}