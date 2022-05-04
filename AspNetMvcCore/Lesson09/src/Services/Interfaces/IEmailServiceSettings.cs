namespace Interfaces;

public interface IEmailServiceSettings
{
    string SmtpLogin { get; set; }
    string SmtpPassword { get; set; }
    int SmtpPort { get; set; }
    string SmtpServer { get; set; }
}
