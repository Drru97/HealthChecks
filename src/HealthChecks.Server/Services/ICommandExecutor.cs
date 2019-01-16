using System.Threading.Tasks;
using HealthChecks.Server.Models;

namespace HealthChecks.Server.Services
{
    public interface ICommandExecutor
    {
        Task<CommandExecutionResult> ExecuteAsync(string command);
    }
}
