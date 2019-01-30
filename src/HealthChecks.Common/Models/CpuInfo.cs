using System.Collections.Generic;

namespace HealthChecks.Common.Models
{
    public class CpuInfo
    {
        public string Architecture { get; set; }
        public int Cores { get; set; }
        public int ThreadsPerCore { get; set; }
        public string Model { get; set; }
        public double Frequency { get; set; }
        public string Virtualization { get; set; }
        public int L1Cache { get; set; }
        public int L2Cache { get; set; }
        public int L3Cache { get; set; }
        public IList<string> Flags { get; set; }
    }
}
