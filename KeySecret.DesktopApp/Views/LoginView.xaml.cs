using KeySecret.DesktopApp.Library.DataAccess;
using System;
using System.Windows;
using System.Windows.Controls;

namespace KeySecret.DesktopApp.Views
{
    public partial class LoginView : Window
    {
        private readonly IServiceProvider _services;
        private readonly IAuthenticateEndpoint _authenticateEndpoint;

        public string Username { get; set; } = "admin";
        public string Password { get; set; } = "Root1207!";
        public string Email { get; set; }

        public LoginView(IServiceProvider services, IAuthenticateEndpoint authenticateEndpoint)
        {
            InitializeComponent();

            DataContext = this;
            _services = services;
            _authenticateEndpoint = authenticateEndpoint;
        }

        private void pwb_PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) => Password = ((PasswordBox)sender).Password;

        private void Exit_Click(object sender, RoutedEventArgs e) => Close();

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                // User error output
                return;
            }

            await _authenticateEndpoint.Authenticate(Username, Password);

            var mainWindow = _services.GetService(typeof(MainWindow)) as MainWindow;
            mainWindow.Show();
            this.Close();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var registerView = _services.GetService(typeof(RegisterView));
            ((RegisterView)registerView).Show();
        }
    }
}