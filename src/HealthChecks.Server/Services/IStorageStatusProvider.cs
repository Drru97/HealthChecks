using System.Threading.Tasks;
using HealthChecks.Server.Models;

namespace HealthChecks.Server.Services
{
    public interface IStorageStatusProvider
    {
        Task<Storage> GetStorageStatusAsync();
    }
}
