using MetricsManager.Entities.Interfaces;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsManager.Quartz
{
    public class QuartzHostedService : IHostedService
    {
        public IScheduler Scheduler { get; set; }

        private readonly ICpuMetricRepository _cpuRepository;
        private readonly IRamMetricRepository _ramRepository;
        private readonly IHddMetricRepository _hddRepository;
        private readonly INetworkMetricRepository _networkRepository;
        private readonly IDotnetMetricRepository _dotnetRepository;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;

        public QuartzHostedService(
            ICpuMetricRepository cpuRepository,
            IRamMetricRepository ramRepository,
            IHddMetricRepository hddRepository,
            INetworkMetricRepository networkRepository,
            IDotnetMetricRepository dotnetRepository,
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IEnumerable<JobSchedule> jobSchedules)
        {
            _cpuRepository = cpuRepository;
            _ramRepository = ramRepository;
            _hddRepository = hddRepository;
            _networkRepository = networkRepository;
            _dotnetRepository = dotnetRepository;
            _schedulerFactory = schedulerFactory;
            _jobSchedules = jobSchedules;
            _jobFactory = jobFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            foreach (var jobSchedule in _jobSchedules)
            {
                var job = CreateJobDetail(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);

                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }

            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private static IJobDetail CreateJobDetail(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(jobType.FullName)
                .WithDescription(jobType.Name)
                .Build();
        }

        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            return TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.JobType.FullName}.trigger")
                .WithCronSchedule(schedule.CronExpression)
                .WithDescription(schedule.CronExpression)
                .Build();
        }
    }
}
