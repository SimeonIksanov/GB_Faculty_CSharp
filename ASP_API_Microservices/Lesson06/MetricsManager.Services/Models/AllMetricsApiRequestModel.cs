namespace MetricsManager.Services.Models
{
    public class AllMetricsApiRequestModel
    {
        public long FromTime { get; set; }
        public long ToTime { get; set; }
        public string ClientBaseAddress { get; set; }

        public int AgentId { get; set; }
    }
}
