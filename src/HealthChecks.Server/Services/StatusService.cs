using System.Threading.Tasks;
using HealthChecks.Common.Models;

namespace HealthChecks.Server.Services
{
    public class StatusService : IStatusService
    {
        private readonly IMemoryStatusProvider _memoryStatus;
        private readonly ICpuStatusProvider _cpuStatus;
        private readonly IStorageStatusProvider _storageStatus;

        public StatusService(IMemoryStatusProvider memoryStatus, ICpuStatusProvider cpuStatus,
            IStorageStatusProvider storageStatus)
        {
            _memoryStatus = memoryStatus;
            _cpuStatus = cpuStatus;
            _storageStatus = storageStatus;
        }

        public async Task<SystemStatus> GetSystemStatusAsync()
        {
            var memory = await _memoryStatus.GetMemoryStatusAsync();
            var cpu = await _cpuStatus.GetCpuStatusAsync();
            var storage = await _storageStatus.GetStorageStatusAsync();

            return new SystemStatus
            {
                MemoryStatus = memory,
                CpuStatus = cpu,
                StorageStatus = storage
            };
        }
    }
}
