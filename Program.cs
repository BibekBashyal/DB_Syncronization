using FleetPanda_task.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace FleetPanda_task
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var serviceProvider = services.BuildServiceProvider();

            ApplicationConfiguration.Initialize();
            Application.Run(serviceProvider.GetRequiredService<Form1>());
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            // Register services with the DI container
            services.AddSingleton<SyncService>(); // Adjust to Singleton or Transient based on your requirement
            services.AddTransient<Form1>();
        }
    }
}
