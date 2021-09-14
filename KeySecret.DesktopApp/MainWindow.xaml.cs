using KeySecret.DesktopApp.Views;
using System;
using System.Windows;

namespace KeySecret.DesktopApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainContentControl.Content = new LoginView();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Categorie_Area.Items.Add(ShowDialog)
        }
    }
}