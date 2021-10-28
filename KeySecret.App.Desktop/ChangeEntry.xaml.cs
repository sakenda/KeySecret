using KeySecret.App.Library;
using KeySecret.App.Library.Models;
using System.Windows;
using System.Windows.Media;

namespace KeySecret.App.Desktop
{
    /// <summary>
    /// Interaction logic for ChangeEntry.xaml
    /// </summary>
    public partial class ChangeEntry : Window
    {
        private readonly IEndpoint<AccountModel> _accountEndpoint;

        public AccountModel Model { get; set; }

        public ChangeEntry(IEndpoint<AccountModel> accountEndpoint, AccountModel model)
        {
            InitializeComponent();
            _accountEndpoint = accountEndpoint;
            Model = model;

            this.DataContext = this;
        }

        private async void Change_Entry(object sender, RoutedEventArgs e)
        {
            await _accountEndpoint.UpdateAsync(Model);
            await ((MainWindow)Application.Current.MainWindow).LoadAccountsAsync();

            this.Close();
        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Name.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void WebadressBox_GotFocus(object sender, RoutedEventArgs e)
        {
            WebAdress.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordTextBlock.Visibility = Visibility.Hidden;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Model.Password = pwb_Password.Password;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}