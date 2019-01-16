using System;
using System.Linq;
using System.Threading.Tasks;
using HealthChecks.Server.Models;

namespace HealthChecks.Server.Services
{
    public class LinuxMemoryStatusProvider : IMemoryStatusProvider
    {
        private readonly ICommandExecutor _commandExecutor;

        private const string MeminfoCommand = "cat /proc/meminfo";
        private const string TotalRamAttribute = "MemTotal";
        private const string FreeRamAttribute = "MemAvailable";
        private const string TotalSwapAttribute = "SwapTotal";
        private const string FreeSwapAttribute = "SwapFree";

        public LinuxMemoryStatusProvider(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        public async Task<Memory> GetMemoryStatusAsync()
        {
            var commandOutput = await _commandExecutor.ExecuteAsync(MeminfoCommand);

            return GetMemoryFromCommandOutput(commandOutput.Output);
        }

        private static Memory GetMemoryFromCommandOutput(string output)
        {
            var outputLines = output.Split(Environment.NewLine);
            var totalRam = KbytesToMbytes(outputLines.Single(el => el.Contains(TotalRamAttribute)).Substring(16).Trim(' ', 'k', 'B'));
            var freeRam = KbytesToMbytes(outputLines.Single(el => el.Contains(FreeRamAttribute)).Substring(16).Trim(' ', 'k', 'B'));
            var totalSwap = KbytesToMbytes(outputLines.Single(el => el.Contains(TotalSwapAttribute)).Substring(16).Trim(' ', 'k', 'B'));
            var freeSwap = KbytesToMbytes(outputLines.Single(el => el.Contains(FreeSwapAttribute)).Substring(16).Trim(' ', 'k', 'B'));

            var memory = new Memory
            {
                TotalRam = totalRam,
                FreeRam = freeRam,
                TotalSwap = totalSwap,
                FreeSwap = freeSwap
            };
            
            return memory;
        }

        private static double KbytesToMbytes(string kbytes)
        {
            const int kbytesInMegabyte = 1024;

            return double.Parse(kbytes) / kbytesInMegabyte;
        }
    }
}
