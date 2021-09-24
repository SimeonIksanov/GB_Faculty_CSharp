using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Services.Models
{
    public class MetricModel
    {
        public int AgentId { get; set; }
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime Time { get; set; }
    }
}
