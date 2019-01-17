using System.Diagnostics;
using System.Threading.Tasks;
using HealthChecks.Server.Models;

namespace HealthChecks.Server.Services.Linux
{
    public class BashCommandExecutor : ICommandExecutor
    {
        public async Task<CommandExecutionResult> ExecuteAsync(string command)
        {
            command = command.Replace("\"", "\"\"");

            var process = CreateBashProcess(command);

            process.Start();
            process.WaitForExit();

            var result = new CommandExecutionResult
            {
                Output = await process.StandardOutput.ReadToEndAsync(),
                Error = await process.StandardError.ReadToEndAsync()
            };

            return result;
        }

        private static Process CreateBashProcess(string command, string bashExecutable = "/bin/bash")
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = bashExecutable,
                    Arguments = "-c \"" + command + "\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };

            return process;
        }
    }
}
