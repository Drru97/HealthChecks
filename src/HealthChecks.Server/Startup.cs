using System;
using HealthChecks.Server.Services;
using HealthChecks.Server.Services.Linux;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthChecks.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            RegisterCommonDependencies(services);

            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Unix:
                    RegisterUnixDependencies(services);
                    break;
                case PlatformID.Win32NT:
                    RegisterWindowsDependencies(services);
                    break;
                default:
                    throw new PlatformNotSupportedException("Current platform is not supported.");
            }
        }

        private static void RegisterCommonDependencies(IServiceCollection services)
        {
            services.AddTransient<ICommandOutputParser, CommandOutputParser>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IStorageStatusProvider, StorageStatusProvider>();
        }

        private static void RegisterUnixDependencies(IServiceCollection services)
        {
            services.AddTransient<ICommandExecutor, BashCommandExecutor>();
            services.AddTransient<IMemoryStatusProvider, LinuxMemoryStatusProvider>();
            services.AddTransient<ICpuStatusProvider, LinuxCpuStatusProvider>();
        }

        private static void RegisterWindowsDependencies(IServiceCollection services)
        {
            // todo: register windows dependencies
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //    app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
