using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace KeySecret.DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        /*
          public event PropertyChangedEventHandler PropertyChanged;

          private string _loginString;
          public string LoginString
          {
              get => _loginString;
              set
              {
                  _loginString = value;
                  OnPropertyChanged(nameof(LoginString));
              }
          }
          */
        public LoginView()
        {
            InitializeComponent();
            // this.DataContext = this;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        /*
protected void OnPropertyChanged(string propertyname) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));

private void Button_Click(object sender, RoutedEventArgs e)
{
LoginString = "TestString";
}
*/
    }
}