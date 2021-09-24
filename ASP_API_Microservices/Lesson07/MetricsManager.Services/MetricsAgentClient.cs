using MetricsManager.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;

namespace MetricsManager.Services
{
    public class MetricsAgentClient : IMetricsAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<MetricsAgentClient> _logger;

        public MetricsAgentClient(HttpClient httpClient, ILogger<MetricsAgentClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public AllMetricsApiResponseModel GetAllRamMetrics(AllMetricsApiRequestModel request)
            => GetAllMetrics(request, "ram");

        public AllMetricsApiResponseModel GetAllHddMetrics(AllMetricsApiRequestModel request)
            => GetAllMetrics(request, "hdd");

        public AllMetricsApiResponseModel GetAllCpuMetrics(AllMetricsApiRequestModel request)
            => GetAllMetrics(request, "cpu");

        public AllMetricsApiResponseModel GetAllDotnetMetrics(AllMetricsApiRequestModel request)
            => GetAllMetrics(request, "dotnet");

        public AllMetricsApiResponseModel GetAllNetworkMetrics(AllMetricsApiRequestModel request)
            => GetAllMetrics(request, "network");

        private AllMetricsApiResponseModel GetAllMetrics(AllMetricsApiRequestModel request,string metric)
        {
            var fromParameter = request.FromTime;
            var toParameter = request.ToTime;
            var httpRequest = new HttpRequestMessage(
                HttpMethod.Get,
                $"{request.ClientBaseAddress}/api/metrics/{metric}/from/{fromParameter}/to/{toParameter}");

            try
            {
                HttpResponseMessage response = _httpClient.SendAsync(httpRequest).Result;

                using var responseStream = response.Content.ReadAsStreamAsync().Result;
                var res = JsonSerializer.DeserializeAsync<AllMetricsApiResponseModel>(
                    responseStream,
                    new JsonSerializerOptions(JsonSerializerDefaults.Web)
                    ).Result;
                return AssignAgentIdNum(res,request.AgentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            
            return null;
        }

        private AllMetricsApiResponseModel AssignAgentIdNum (AllMetricsApiResponseModel data, int agentId)
        {
            foreach (var item in data.Metrics)
            {
                item.AgentId = agentId;
            }
            return data;
        }
    }

}
