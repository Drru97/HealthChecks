namespace HealthChecks.Common.Models
{
    public class Cpu
    {
        public double CurrentUsage { get; set; }

        public double AverageUsageFor1M { get; set; }
        public double AverageUsageFor5M { get; set; }
        public double AverageUsageFor15M { get; set; }

        public int RunningProcesses { get; set; }
        public int TotalProcesses { get; set; }

        public CpuInfo CpuInfo { get; set; }
    }
}
