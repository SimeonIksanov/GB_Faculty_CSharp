namespace Interfaces;

public interface INotificationService
{
    Task SendAsync(IEnumerable<string> to, string message, string subject = "");
}
