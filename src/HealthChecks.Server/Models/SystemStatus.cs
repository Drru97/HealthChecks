namespace HealthChecks.Server.Models
{
    public class SystemStatus
    {
        public Cpu CpuStatus { get; set; }
        public Memory MemoryStatus { get; set; }
        public Storage StorageStatus { get; set; }
    }
}
