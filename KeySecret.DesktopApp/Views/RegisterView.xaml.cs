using KeySecret.DesktopApp.Library.Interfaces;
using System.Windows;

namespace KeySecret.DesktopApp.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        private IApiHelper _apiHelper { get; }

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsErrorVisible { get; set; }

        public RegisterView(IApiHelper apiHelper)
        {
            InitializeComponent();

            DataContext = this;
            _apiHelper = apiHelper;
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

            var response = await _apiHelper.Register(Username, Email, Password);
            IsErrorVisible = true;
            ErrorMessage.Text = $"{response.Status}: {response.Message}";
        }
    }
}