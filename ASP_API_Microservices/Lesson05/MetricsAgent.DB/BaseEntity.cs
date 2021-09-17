using System;

namespace MetricsAgent.DB
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime Time { get; set; }
    }
}
