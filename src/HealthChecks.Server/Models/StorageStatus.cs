namespace HealthChecks.Server.Models
{
    public class StorageStatus
    {
        public string Filesystem { get; set; }
        public string MountedOn { get; set; }
        public double TotalSpace { get; set; }
        public double UsedSpace { get; set; }
        public double AvailableSpace { get; set; }
        public int CapacityPercentage { get; set; }
    }
}
