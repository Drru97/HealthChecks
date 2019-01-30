using System.Threading.Tasks;
using HealthChecks.Common.Models;

namespace HealthChecks.Server.Services
{
    public interface IMemoryStatusProvider
    {
        Task<Memory> GetMemoryStatusAsync();
    }
}
