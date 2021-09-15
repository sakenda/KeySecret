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
            MainWindow._categorie = KategorieBox.Text;
            Close();
        }

        private void KategorieBox_GotFocus(object sender, RoutedEventArgs e)
        {
            KategorieBox.Foreground = new SolidColorBrush(Colors.Black);
            KategorieBox.Clear();
        }
    }
}
