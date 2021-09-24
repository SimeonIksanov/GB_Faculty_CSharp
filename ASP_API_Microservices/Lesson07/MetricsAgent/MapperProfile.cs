using AutoMapper;
using MetricsAgent.DB;
using MetricsAgent.Requests;
using System;
using MetricsAgent.Responses;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, MetricDto>().ForMember(m=>m.Time,option=>option.MapFrom(src=>DateTimeOffset.FromUnixTimeSeconds(src.Time).DateTime));
            CreateMap<DotnetMetric, MetricDto>().ForMember(m => m.Time, option => option.MapFrom(src => DateTimeOffset.FromUnixTimeSeconds(src.Time).DateTime));
            CreateMap<HddMetric, MetricDto>().ForMember(m=>m.Time,option=>option.MapFrom(src=>DateTimeOffset.FromUnixTimeSeconds(src.Time).DateTime));
            CreateMap<NetworkMetric, MetricDto>().ForMember(m=>m.Time,option=>option.MapFrom(src=>DateTimeOffset.FromUnixTimeSeconds(src.Time).DateTime));
            CreateMap<RamMetric, MetricDto>().ForMember(m=>m.Time,option=>option.MapFrom(src=>DateTimeOffset.FromUnixTimeSeconds(src.Time).DateTime));
            //CreateMap<CreateMetricRequest, CpuMetric>();
            //CreateMap<CreateMetricRequest, RamMetric>();
            //CreateMap<CreateMetricRequest, HddMetric>();
            //CreateMap<CreateMetricRequest, NetworkMetric>();
            //CreateMap<CreateMetricRequest, DotnetMetric>();
        }
    }
}
