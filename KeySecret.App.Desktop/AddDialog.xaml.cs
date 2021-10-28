using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace KeySecret.App.Desktop
{
    /// <summary>
    /// Interaktionslogik für AddDialog.xaml
    /// </summary>
    public partial class AddDialog : Window
    {
        public AddDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void KategorieBox_GotFocus(object sender, RoutedEventArgs e)
        {
            KategorieBox.Foreground = new SolidColorBrush(Colors.Black);
            KategorieBox.Clear();
        }

        private void KeyDown_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Close();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}