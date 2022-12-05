namespace ScannerEmulator
{
    public interface IPerfData
    {
        int CpuMetric { get; }
        int MemoryMetric { get; }
    }
}