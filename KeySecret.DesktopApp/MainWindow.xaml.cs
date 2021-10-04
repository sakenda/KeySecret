using KeySecret.DesktopApp.Library.Accounts.Models;
using KeySecret.DesktopApp.Library.Interfaces;

using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KeySecret.DesktopApp
{
    public partial class MainWindow : Window
    {
        public static string _categorie { get; set; }
        public static ObservableCollection<AccountModel> AccountsList { get; set; }

        private readonly IEndpoint<AccountModel> _accountEndpoint;

        public MainWindow(IEndpoint<AccountModel> accountEndpoint)
        {
            InitializeComponent();

            _accountEndpoint = accountEndpoint;

            AccountsList = new ObservableCollection<AccountModel>();
            lb_Accounts.ItemsSource = AccountsList;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) => this.DragMove();

        private void Exit_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);

        private void Add(object sender, RoutedEventArgs e)
        {
            AddDialog _dialogBox = new AddDialog();
            _dialogBox.ShowDialog();
            Categorie_Area.Items.Add(_categorie);
        }

        private void Remove(object sender, RoutedEventArgs e)
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

        public async Task LoadAccountsAsync()
        {
            if (AccountsList == null)
                AccountsList = new ObservableCollection<AccountModel>();

            AccountsList.Clear();

            foreach (var item in await _accountEndpoint.GetAllAsync())
            {
                AccountsList.Add(item);
            }
        }

        private void TestUpdateAccount_OnClick(object sender, RoutedEventArgs e)
        {
            var item = new AccountModel();
            item.Id = AccountsList[2].Id;
            item.Name = AccountsList[2].Name + "*updated*";
            item.WebAdress = AccountsList[2].WebAdress;
            item.Password = AccountsList[2].Password;

            _accountEndpoint.UpdateAsync(item);
        }

        private void NewPw_Click(object sender, RoutedEventArgs e)
        {
            AddPw _addPw = new AddPw(_accountEndpoint);
            _addPw.ShowDialog();
        }
    }
}