using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;

namespace KeySecret.DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl, INotifyPropertyChanged
    {
        public LoginView()
        {
            InitializeComponent();
            UsernameBox.GotFocus += new RoutedEventHandler(UsernameBox_GotFocus);
            PasswordBox.GotFocus += new RoutedEventHandler(PasswordBox_GotFocus);
        }

        private void UsernameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UsernameBox.Foreground = new SolidColorBrush(Colors.Black);
            if (UsernameBox.Text is "Username")
            {
                UsernameBox.Text = null;
            }
        }

        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox.Foreground = new SolidColorBrush(Colors.Black);
            PasswordBox.Clear();
        }
    }
}