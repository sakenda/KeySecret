using KeySecret.DesktopApp.Library.DataAccess;
using KeySecret.DesktopApp.Library.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace KeySecret.DesktopApp
{
    public partial class MainWindow : Window
    {
        public static string _categorie { get; set; }

        private IAccountEndpoint _accountEndpoint;
        private ObservableCollection<AccountModel> _accountsList;

        public ObservableCollection<AccountModel> AccountsList
        {
            get { return _accountsList; }
            set { _accountsList = value; }
        }

        public MainWindow(IAccountEndpoint accountEndpoint)
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