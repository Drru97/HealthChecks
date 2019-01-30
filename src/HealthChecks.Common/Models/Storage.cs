using System.Collections.Generic;

namespace HealthChecks.Common.Models
{
    public class Storage
    {
        public IList<StorageStatus> Storages { get; set; } = new List<StorageStatus>();
    }
}
