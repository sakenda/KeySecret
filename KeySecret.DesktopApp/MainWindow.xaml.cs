using KeySecret.DesktopApp.Views;
using System;
using System.Windows;
using System.Windows.Input;

namespace KeySecret.DesktopApp
{
    public partial class MainWindow : Window
    {
        public static string _categorie { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            //MainContentControl.Content = new LoginView();
        }

        private void Exit_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);

        private void add(object sender, RoutedEventArgs e)
        {
            AddDialog _dialogBox = new AddDialog();
            _dialogBox.ShowDialog();
            Categorie_Area.Items.Add(_categorie);
        }

        private void remove(object sender, RoutedEventArgs e)
        {

            if (Categorie_Area.SelectedItem == null || Categorie_Area.SelectedItem.Equals(Allgemein))
            {
                return;
            }
            Categorie_Area.Items.Remove(Categorie_Area.SelectedItem);



        }
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}