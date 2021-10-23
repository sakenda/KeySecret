using KeySecret.DesktopApp.Library.Models;
using KeySecret.DesktopApp.Library.DataAccess;
using KeySecret.DesktopApp.Library.Helper;
using KeySecret.DesktopApp.Library;
using KeySecret.DesktopApp.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Windows;

namespace KeySecret.DesktopApp
{
    public partial class App : Application
    {
        public static IApiHelper ApiHelper { get; private set; }
        public IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            ApiHelper = new ApiHelper();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            //var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            var mainWindow = ServiceProvider.GetRequiredService<LoginView>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IApiHelper, ApiHelper>()
                    .AddSingleton<ICurrentUser, CurrentUser>()
                    .AddSingleton<IAuthenticateEndpoint, AuthenticateEndpoint>()
                    .AddSingleton<IEndpoint<AccountModel>, AccountEndpoint>()
                    .AddSingleton<IEndpoint<CategoryModel>, CategoriesEndpoint>();

            services.AddTransient(typeof(LoginView))
                    .AddTransient(typeof(RegisterView))
                    .AddTransient(typeof(MainWindow));
        }
    }
}