using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KeySecret.App.Library;
using KeySecret.App.Library.Helper;
using KeySecret.App.Library.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KeySecret.App.Web.Pages
{
    public class MainViewModel
    {
        private readonly ILogger<MainViewModel> _logger;
        private readonly IApiHelper _apiHelper;
        private readonly IEndpoint<AccountModel> _accountEndpoint;
        private readonly IEndpoint<CategoryModel> _categoryEndpoint;

        public IApiHelper ApiHelper => _apiHelper;

        [BindProperty] public CurrentUser CurrentUser => _apiHelper.CurrentUser;
        [BindProperty] public List<AccountModel> AccountsList { get; set; }
        [BindProperty] public List<CategoryModel> CategoriesList { get; set; }

        public MainViewModel(ILogger<MainViewModel> logger, IApiHelper apiHelper, IEndpoint<AccountModel> accountEndpoint, IEndpoint<CategoryModel> categoryEndpoint)
        {
            _apiHelper = apiHelper;
            _accountEndpoint = accountEndpoint;
            _categoryEndpoint = categoryEndpoint;
            _logger = logger;

            InitializeViewModel();
        }

        public async void InitializeViewModel()
        {
            AccountsList = new List<AccountModel>(await _accountEndpoint.GetAllAsync());
            CategoriesList = new List<CategoryModel>(await _categoryEndpoint.GetAllAsync());
        }

        public async Task CreateNewAccount(AccountModelSelected model)
        {
            Guid.TryParse(model.CategoryId, out Guid categoryId);

            CategoryModel category = null;
            if (categoryId != Guid.Empty)
                category = await _categoryEndpoint.GetById(categoryId);

            var account = AccountModel.SelectedAsAccountModel(model, category);

            await _accountEndpoint.InsertAsync(account);

            AccountsList.Add(account);
        }

        public async Task DeleteAccount(AccountModelSelected account)
        {
            await _accountEndpoint.DeleteAsync(account.Id);

            var rAccount = AccountsList.FirstOrDefault(a => a.Id == account.Id);
            AccountsList.Remove(rAccount);
        }

        public async Task UpdateAccount(AccountModelSelected account)
        {
            Guid.TryParse(account.CategoryId, out Guid categoryId);
            var category = CategoriesList.FirstOrDefault(c => c.Id == categoryId);
            var uAccount = AccountModel.SelectedAsAccountModel(account, category);

            int index = AccountsList.FindIndex(a => a.Id == account.Id);
            if (index != -1)
                AccountsList[index] = uAccount;

            await _accountEndpoint.UpdateAsync(uAccount);
        }
    }
}