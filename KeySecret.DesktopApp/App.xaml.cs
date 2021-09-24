using KeySecret.DesktopApp.Library.Accounts.Models;
using KeySecret.DesktopApp.Library.DataAccess;
using KeySecret.DesktopApp.Library.Helper;
using KeySecret.DesktopApp.Library.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Windows;

namespace KeySecret.DesktopApp
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IApiHelper, ApiHelper>()
                    .AddSingleton<IEndpoint<AccountModel, UpdateAccountModel>, AccountEndpoint>();

            services.AddTransient(typeof(MainWindow));
        }
    }
}