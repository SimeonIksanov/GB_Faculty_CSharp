using Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Helpers
{
    public class EmailServiceSettings : IEmailServiceSettings
    {
        public EmailServiceSettings()
        {
        }

        public EmailServiceSettings(IOptions<EmailServiceSettings> settings)
        {
            SmtpServer = settings.Value.SmtpServer;
            SmtpPort = settings.Value.SmtpPort;
            SmtpLogin = settings.Value.SmtpLogin;
            SmtpPassword = settings.Value.SmtpPassword;
        }

        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpLogin { get; set; }
        public string SmtpPassword { get; set; }
    }
}
