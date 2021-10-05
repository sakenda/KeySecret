using KeySecret.DesktopApp.Library.Accounts.Models;
using KeySecret.DesktopApp.Library.DataAccess;
using KeySecret.DesktopApp.Library.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KeySecret.DesktopApp
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