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
        public string NameBox { get; set; } = "Name";
        public string WebadressBox { get; set; } = "Webadress";
        public string PasswordBox { get; set; } = "";

        private IEndpoint<AccountModel> _accountEndpoint;
        public AddPw(IEndpoint<AccountModel> accountEndpoint)
        {
            InitializeComponent();
            _accountEndpoint = accountEndpoint;
            this.DataContext = this;
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordTextBlock.Visibility = Visibility.Hidden;
        }

        private void NameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            Name.Foreground = new SolidColorBrush(Colors.Black);
            Name.Clear();
        }

        private void WebAdress_GotFocus(object sender, RoutedEventArgs e)
        {
            Webadress.Foreground = new SolidColorBrush(Colors.Black);
            Webadress.Clear();
        }

        private async void New_Entry_Click(object sender, RoutedEventArgs e)
        {
            AccountModel model = new AccountModel();

            model.Name = NameBox;
            model.WebAdress = WebadressBox;
            model.Password = PasswordBox;

            await _accountEndpoint.InsertAsync(model);
            await ((MainWindow)Application.Current.MainWindow).LoadAccountsAsync();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
                ((dynamic)this.DataContext).PasswordBox = ((PasswordBox)sender).Password;
        }
    }
}
