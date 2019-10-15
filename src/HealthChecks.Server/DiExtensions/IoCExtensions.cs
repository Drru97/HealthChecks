using HealthChecks.Server.Services;
using HealthChecks.Server.Services.Linux;
using HealthChecks.Server.Services.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace HealthChecks.Server.DiExtensions
{
    public static class IoCExtensions
    {
        public static void RegisterUnixDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICommandExecutor, BashCommandExecutor>();
            services.AddTransient<IMemoryStatusProvider, LinuxMemoryStatusProvider>();
            services.AddTransient<ICpuStatusProvider, LinuxCpuStatusProvider>();
        }

        public static void RegisterWindowsDependencies(this IServiceCollection services)
        {
            // todo: register windows dependencies
            services.AddTransient<ICpuStatusProvider, WindowsCpuStatusProvider>();
        }

        public static void RegisterCommonDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICommandOutputParser, CommandOutputParser>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IStorageStatusProvider, StorageStatusProvider>();
        }
    }
}
