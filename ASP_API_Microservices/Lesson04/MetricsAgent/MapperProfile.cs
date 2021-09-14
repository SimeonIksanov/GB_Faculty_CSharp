using AutoMapper;
using MetricsAgent.DB;
using MetricsAgent.Requests;
//using MetricsAgent.DAL.Models;
using MetricsAgent.Responses;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, MetricDto>();
            CreateMap<DotnetMetric, MetricDto>();
            CreateMap<HddMetric, MetricDto>();
            CreateMap<NetworkMetric, MetricDto>();
            CreateMap<RamMetric, MetricDto>();
            CreateMap<CreateMetricRequest, CpuMetric>();
            CreateMap<CreateMetricRequest, RamMetric>();
            CreateMap<CreateMetricRequest, HddMetric>();
            CreateMap<CreateMetricRequest, NetworkMetric>();
            CreateMap<CreateMetricRequest, DotnetMetric>();
        }
    }
}
