using MetricsManager.Services.Models;

namespace MetricsManager.Services
{
    public interface IMetricsAgentClient
    {
        public AllMetricsApiResponseModel GetAllRamMetrics(AllMetricsApiRequestModel request);

        public AllMetricsApiResponseModel GetAllHddMetrics(AllMetricsApiRequestModel request);

        public AllMetricsApiResponseModel GetAllDotnetMetrics(AllMetricsApiRequestModel request);

        public AllMetricsApiResponseModel GetAllCpuMetrics(AllMetricsApiRequestModel request);

        public AllMetricsApiResponseModel GetAllNetworkMetrics(AllMetricsApiRequestModel request);
    }
}
