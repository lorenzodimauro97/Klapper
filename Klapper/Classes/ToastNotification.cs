using Radzen;

namespace Klapper.Shared;

public class ToastNotification
{
    public static void Notificate(NotificationService Toast, bool result, string goodMessage = "",
        string badMessage = "")
    {
        Toast.Notify(new NotificationMessage
        {
            Severity = result ? NotificationSeverity.Success : NotificationSeverity.Error,
            Detail = result ? goodMessage : badMessage,
            Summary = result ? "Success" : "Error",
            Duration = 7000
        });
    }
    
    public static void NotificateInfo(NotificationService Toast, string message)
    {
        Toast.Notify(new NotificationMessage
        {
            Severity = NotificationSeverity.Info,
            Detail = message,
            Summary = "Information",
            Duration = 7000
        });
    }
}