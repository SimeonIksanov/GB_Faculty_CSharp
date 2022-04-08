using System;
namespace ScannerEmulator
{
    public class PerfData : IPerfData
    {
        private static readonly Random s_random = new Random();

        public PerfData()
        {
            CpuMetric = s_random.Next(101);
            MemoryMetric = s_random.Next(50);
        }

        public int CpuMetric { get; }

        public int MemoryMetric { get; }

        public override string ToString()
        {
            return $"cpu:{CpuMetric}; mem:{MemoryMetric}";
        }
    }
}
