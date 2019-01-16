using System.Collections.Generic;

namespace HealthChecks.Server.Models
{
    public class Storage
    {
        public IList<StorageStatus> Storages { get; set; } = new List<StorageStatus>();
    }
}
