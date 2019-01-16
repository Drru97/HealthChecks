using System.Threading.Tasks;
using HealthChecks.Server.Models;

namespace HealthChecks.Server.Services
{
    public interface ICpuStatusProvider
    {
        Task<Cpu> GetCpuStatusAsync();
    }
}
