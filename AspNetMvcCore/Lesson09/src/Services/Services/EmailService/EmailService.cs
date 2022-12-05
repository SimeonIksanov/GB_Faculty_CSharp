using Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Services.EmailService;

public class EmailService : INotificationService, IDisposable
{
    private readonly IEmailServiceSettings _settings;
    private readonly SmtpClient _smtpClient;

    public EmailService(IEmailServiceSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _smtpClient = new SmtpClient();
        _smtpClient.Connect(_settings.SmtpServer, _settings.SmtpPort, SecureSocketOptions.SslOnConnect);
        _smtpClient.Authenticate(_settings.SmtpLogin, _settings.SmtpPassword);
    }

    public void Dispose()
    {
        _smtpClient.Disconnect(true);
        _smtpClient.Dispose();
    }

    public async Task SendAsync(IEnumerable<string> to, string message, string subject = "")
    {
        if (to is null)
            throw new ArgumentNullException(nameof(to));

        MimeMessage email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_settings.SmtpLogin));
        foreach (string toItem in to)
        {
            email.To.Add(MailboxAddress.Parse(toItem));
        }

        email.Subject = subject;

        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = message
        };
        await _smtpClient.SendAsync(email);

    }
}
