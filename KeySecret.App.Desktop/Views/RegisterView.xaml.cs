using KeySecret.App.Library.DataAccess;
using System.Windows;

namespace KeySecret.App.Desktop.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        private IAuthenticateEndpoint _authenticateEndpoint { get; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsErrorVisible { get; set; }

        public RegisterView(IAuthenticateEndpoint authenticateEndpoint)
        {
            InitializeComponent();

            DataContext = this;
            _authenticateEndpoint = authenticateEndpoint;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e) => this.Close();

        private async void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Textvalidierung, Error anzeige

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                IsErrorVisible = true;
                ErrorMessage.Text = "Bitte alle Felder ausfüllen";
                return;
            }

            var response = await _authenticateEndpoint.Register(Username, Email, Password);
            IsErrorVisible = true;
            ErrorMessage.Text = $"{response.Status}: {response.Message}";
        }
    }
}