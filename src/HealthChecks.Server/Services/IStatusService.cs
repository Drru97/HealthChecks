using System.Threading.Tasks;
using HealthChecks.Server.Models;

namespace HealthChecks.Server.Services
{
    public interface IStatusService
    {
        Task<SystemStatus> GetSystemStatusAsync();
    }
}
