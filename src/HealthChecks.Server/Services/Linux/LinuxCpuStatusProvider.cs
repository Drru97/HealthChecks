using System;
using System.Linq;
using System.Threading.Tasks;
using HealthChecks.Server.Models;

namespace HealthChecks.Server.Services.Linux
{
    public class LinuxCpuStatusProvider : ICpuStatusProvider
    {
        private readonly ICommandExecutor _commandExecutor;

        private const string CpuLoadCommand = "cat /proc/loadavg";
        private const string CpuInfoCommand = "cat /proc/cpuinfo";
        private const string CpuNameAttribute = "model name";
        private const string CpuCoresAttribute = "processor";

        public LinuxCpuStatusProvider(ICommandExecutor commandExecutor)
        {
            _commandExecutor = commandExecutor;
        }

        public async Task<Cpu> GetCpuStatusAsync()
        {
            var cpu = await GetCpuFromCommandOutput();

            return cpu;
        }

        private async Task<Cpu> GetCpuFromCommandOutput()
        {
            var cpuLoadOutput = await _commandExecutor.ExecuteAsync(CpuLoadCommand);
            var loadParameters = cpuLoadOutput.Output.Split(' ');

            var cpuInfoOutput = await _commandExecutor.ExecuteAsync(CpuInfoCommand);
            var cpuInfoLines = cpuInfoOutput.Output.Split(Environment.NewLine);
            // TODO: now we checks only first core
            var cpuName = cpuInfoLines.First(el => el.Contains(CpuNameAttribute)).Substring(12).Trim();
            var cpuCores = cpuInfoLines.Last(el => el.Contains(CpuCoresAttribute)).Substring(12).Trim();

            var cpu = new Cpu
            {
                AverageUsageFor1M = double.Parse(loadParameters[0].Trim()),
                AverageUsageFor5M = double.Parse(loadParameters[1].Trim()),
                AverageUsageFor15M = double.Parse(loadParameters[2].Trim()),
                RunningProcesses = int.Parse(loadParameters[3].Split('/').First()),
                TotalProcesses = int.Parse(loadParameters[3].Split('/').Last()),
                CpuName = cpuName,
                Cores = int.Parse(cpuCores) + 1
            };

            return cpu;
        }
    }
}
