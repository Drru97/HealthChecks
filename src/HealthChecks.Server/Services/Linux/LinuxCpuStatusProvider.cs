using System;
using System.Linq;
using System.Threading.Tasks;
using HealthChecks.Common.Models;

namespace HealthChecks.Server.Services.Linux
{
    public class LinuxCpuStatusProvider : ICpuStatusProvider
    {
        private readonly ICommandExecutor _commandExecutor;
        private readonly ICommandOutputParser _outputParser;

        private const string CpuLoadCommand = "cat /proc/loadavg";

        //   private const string CpuInfoCommand = "cat /proc/cpuinfo";
        private const string CpuInfoCommand = "cat /proc/cpuinfo";
        private const string CpuNameAttribute = "model name";
        private const string CpuCoresAttribute = "processor";

        public LinuxCpuStatusProvider(ICommandExecutor commandExecutor, ICommandOutputParser outputParser)
        {
            _commandExecutor = commandExecutor;
            _outputParser = outputParser;
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
            var cpuInfo = new CpuInfo
            {
                Architecture = _outputParser.Parse(cpuInfoLines, "Architecture"),
                Cores = int.Parse(_outputParser.Parse(cpuInfoLines, "Cores")),
                Model = _outputParser.Parse(cpuInfoLines, "Model name")
            };

            var cpu = new Cpu
            {
                AverageUsageFor1M = double.Parse(loadParameters[0].Trim()),
                AverageUsageFor5M = double.Parse(loadParameters[1].Trim()),
                AverageUsageFor15M = double.Parse(loadParameters[2].Trim()),
                RunningProcesses = int.Parse(loadParameters[3].Split('/').First()),
                TotalProcesses = int.Parse(loadParameters[3].Split('/').Last()),
                CpuInfo = cpuInfo
            };

            return cpu;
        }
    }
}
