namespace HealthChecks.Server.Models
{
    public class Cpu
    {
        public string CpuName { get; set; }
        public int Cores { get; set; }

        public double AverageUsageFor1M { get; set; }
        public double AverageUsageFor5M { get; set; }
        public double AverageUsageFor15M { get; set; }

        public int RunningProcesses { get; set; }
        public int TotalProcesses { get; set; }
    }
}
