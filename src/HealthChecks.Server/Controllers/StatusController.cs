using System.Diagnostics;
using System.Threading.Tasks;
using HealthChecks.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthChecks.Server.Controllers
{
    [Route("status")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public async Task<IActionResult> Status([FromQuery] string command)
        {
//            var cpu = new System.Diagnostics.PerformanceCounter("Processor", "% Processor Time", "_Total");
//            var memory = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes");
//
//            var firstCpu = cpu.NextValue();
//            await Task.Delay(1);
//            var secondCpu = cpu.NextValue();
//            
//            var data = $"CPU: {secondCpu} %\t RAM: {memory.NextValue()} MB.";

            var status = await _statusService.GetSystemStatusAsync();
            var data = ExecuteBashCommand(command);

            return Ok(status);
        }

        private static string ExecuteBashCommand(string command)
        {
            command = command.Replace("\"", "\"\"");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = "-c \"" + command + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            return process.StandardOutput.ReadToEnd();
        }
    }
}
