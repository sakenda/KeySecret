using KeySecret.DesktopApp.Library.Interfaces;
using KeySecret.DesktopApp.Library.Models;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

namespace KeySecret.DesktopApp
{
    public partial class MainWindow : Window
    {
        public static string _categorie { get; set; }

        private IEndpoint<AccountModel> _accountEndpoint;
        private ObservableCollection<AccountModel> _accountsList;

        public ObservableCollection<AccountModel> AccountsList
        {
            get => _accountsList;
            set
            {
                _accountsList = value;
            }
        }

        public MainWindow(IEndpoint<AccountModel> accountEndpoint)
        {
            InitializeComponent();

            _accountEndpoint = accountEndpoint;

            AccountsList = new ObservableCollection<AccountModel>();
            lb_Accounts.ItemsSource = AccountsList;
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

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadAccountsAsync();
        }

        private async Task LoadAccountsAsync()
        {
            if (AccountsList == null)
                AccountsList = new ObservableCollection<AccountModel>();

            var list = await _accountEndpoint.GetAll();

            foreach (var item in list)
            {
                AccountsList.Add(item);
            }
        }
    }
}