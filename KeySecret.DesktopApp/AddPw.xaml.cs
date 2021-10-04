using KeySecret.DesktopApp.Library.Accounts.Models;
using KeySecret.DesktopApp.Library.DataAccess;
using KeySecret.DesktopApp.Library.Interfaces;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddPw.xaml
    /// </summary>
    public partial class AddPw : Window
    {
        private IEndpoint<AccountModel> _accountEndpoint;
        public AddPw(IEndpoint<AccountModel> accountEndpoint)
        {
            InitializeComponent();
            _accountEndpoint = accountEndpoint;
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordTextBlock.Visibility = Visibility.Hidden;
        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void WebAdress_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private async void New_Entry_Click(object sender, RoutedEventArgs e)
        {
            AccountModel model = new AccountModel();

            model.Name = NameBox.Text;
            model.WebAdress = WebadressBox.Text;
            model.Password = PasswordBox.Password;

            await _accountEndpoint.InsertAsync(model);
            await ((MainWindow)Application.Current.MainWindow).LoadAccountsAsync();
        }
    }
}
