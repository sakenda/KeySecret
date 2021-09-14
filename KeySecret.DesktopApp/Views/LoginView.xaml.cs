using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeySecret.DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {

        public LoginView()
        {
            InitializeComponent();
           
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
