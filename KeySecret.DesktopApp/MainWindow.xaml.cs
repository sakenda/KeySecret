using KeySecret.DesktopApp.Library.DataAccess;
using KeySecret.DesktopApp.Library.Models;
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
            _accountEndpoint = accountEndpoint;
            //AccountsList = new List<AccountModel>(_accountEndpoint.GetAllAccounts().Result);
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
            try
            {

                if (Categorie_Area.SelectedItem.Equals(Allgemein))
                {
                    return;
                }
                Categorie_Area.Items.Remove(Categorie_Area.SelectedItem);
            }
            catch (NullReferenceException)
            {
                return;
            }

        }
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}