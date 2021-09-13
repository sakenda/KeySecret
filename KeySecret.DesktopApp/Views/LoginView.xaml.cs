using System.ComponentModel;
using System.Windows.Controls;

namespace KeySecret.DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl, INotifyPropertyChanged
    {
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

        public LoginView()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        protected void OnPropertyChanged(string propertyname) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            LoginString = "TestString";
        }
    }
}
