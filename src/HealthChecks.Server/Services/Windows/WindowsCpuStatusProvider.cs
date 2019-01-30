using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HealthChecks.Common.Models;

namespace HealthChecks.Server.Services.Windows
{
    public class WindowsCpuStatusProvider : ICpuStatusProvider
    {
        private readonly PerformanceCounter _cpuCounter;

        private const string CounterCategory = "Processor";
        private const string CounterName = "% Processor Time";
        private const string CounterInstance = "_Total";

        public WindowsCpuStatusProvider()
        {
            _cpuCounter = new PerformanceCounter(CounterCategory, CounterName, CounterInstance, true);
        }

        public async Task<Cpu> GetCpuStatusAsync()
        {
            var usage = _cpuCounter.NextValue();
            if (usage < 0.1)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(500));
                usage = _cpuCounter.NextValue();
            }

            var cpu = new Cpu
            {
                CurrentUsage = usage
            };

            return cpu;
        }
    }
}
