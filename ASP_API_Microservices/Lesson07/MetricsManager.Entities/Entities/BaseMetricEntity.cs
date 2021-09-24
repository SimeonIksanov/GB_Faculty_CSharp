using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetricsManager.Entities.Entities
{
    public abstract class BaseMetricEntity
    {
        public int Id { get; set; }

        public int AgentId { get; set; }
        public int Value { get; set; }
        public long Time { get; set; }
    }
}
