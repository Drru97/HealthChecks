using System.Threading.Tasks;
using HealthChecks.Common.Models;

namespace HealthChecks.Server.Services
{
    public interface IStatusService
    {
        Task<SystemStatus> GetSystemStatusAsync();
    }
}
