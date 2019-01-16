namespace HealthChecks.Server.Models
{
    public class Memory
    {
        public double TotalRam { get; set; }
        public double FreeRam { get; set; }
        public double UsedRam => TotalRam - FreeRam;

        public double TotalSwap { get; set; }
        public double FreeSwap { get; set; }
        public double UsedSwap => TotalSwap - FreeSwap;
    }
}
