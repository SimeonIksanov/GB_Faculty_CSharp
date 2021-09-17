using MetricsAgent.DB;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetricsAgent.PerfSerivce
{
    public class QuartzHostedService : IHostedService
    {
        private readonly IDbRepository<CpuMetric> _cpuRepository;
        private readonly IDbRepository<RamMetric> _ramRepository;
        private readonly IDbRepository<HddMetric> _hddRepository;
        private readonly IDbRepository<NetworkMetric> _networkRepository;
        private readonly IDbRepository<DotnetMetric> _dotnetRepository;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly IJobFactory _jobFactory;
        private readonly IEnumerable<JobSchedule> _jobSchedules;

        public QuartzHostedService(
            IDbRepository<CpuMetric> cpuRepository,
            IDbRepository<RamMetric> ramRepository,
            IDbRepository<HddMetric> hddRepository,
            IDbRepository<NetworkMetric> networkRepository,
            IDbRepository<DotnetMetric> dotnetRepository,
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
        public IScheduler Scheduler { get; set; }

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
