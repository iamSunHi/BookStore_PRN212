using BulkyBook.ServiceManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;

namespace BulkyBook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider? ServiceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configurationRoot = configBuilder.Build();

            var dependencyInjection = new DependencyInjection();
            dependencyInjection.ConfigureServices(services, configurationRoot, "DefaultConnection");
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            if (ServiceProvider is not null)
            {
                var loginWindow = ServiceProvider.GetService<LoginWindow>();
                loginWindow?.Show();
            }
        }
    }

}
