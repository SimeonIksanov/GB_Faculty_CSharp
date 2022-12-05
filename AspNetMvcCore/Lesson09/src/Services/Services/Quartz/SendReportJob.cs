using System;
using Domain.Entities;
using Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;
using Services.EmailService;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Quartz
{
    public class SendReportJob : IJob
    {
        private readonly ILogger<SendReportJob> _logger;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Report> _reportRepository;
        private readonly INotificationService _notificationService;
        private readonly IReport _reportGenerator;

        public SendReportJob(ILogger<SendReportJob> logger,
                             IRepository<User> userRepository,
                             IRepository<Report> reportRepository,
                             INotificationService notificationService,
                             IReport reportGenerator)
        {
            _logger = logger;
            _userRepository = userRepository;
            _reportRepository = reportRepository;
            _notificationService = notificationService;
            _reportGenerator = reportGenerator;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var reportsToSend = (await _reportRepository.GetAllAsync())
                                                 .Where(r => r.SendAt <= DateTime.Now && r.IsSent == false)
                                                 .ToList();
            if (!reportsToSend.Any())
                return;

            var registeredUsers = (await _userRepository.GetAllAsync()).ToList();
            if (!registeredUsers.Any())
                return;

            foreach (var report in reportsToSend)
            {
                await _notificationService.SendAsync(
                    registeredUsers.Select(x => x.Email),
                    _reportGenerator.Create(report),
                    report.Title);

                _logger.LogInformation("Sent report '{0}' by email", report.Title);
                
                report.IsSent = true;
                await _reportRepository.UpdateAsync(report);
            }
        }
    }
}
