using KeySecret.DesktopApp.Library.Authentification.Models;
using KeySecret.DesktopApp.Library.Interfaces;
using System;
using System.Windows;
using System.Windows.Controls;

namespace KeySecret.DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly IApiHelper _apiHelper;
        private readonly IServiceProvider _services;

        public string Username { get; set; } = "admin";
        public string Password { get; set; } = "Root1207!";
        public string Email { get; set; }

        public LoginView(IApiHelper apiHelper, IServiceProvider services)
        {
            InitializeComponent();

            DataContext = this;
            _apiHelper = apiHelper;
            _services = services;
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

            await _apiHelper.Authenticate(Username, Password);

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