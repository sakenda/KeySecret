using System;
using System.Collections.Generic;
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

        public async Task InitializeViewModel()
        {
            AccountsList = new List<AccountModel>(await _accountEndpoint.GetAllAsync());
            CategoriesList = new List<CategoryModel>(await _categoryEndpoint.GetAllAsync());
        }

        public async Task CreateNewAccount(AccountModel model, Guid categoryId)
        {
            CategoryModel category = null;

            if (categoryId != Guid.Empty)
                category = await _categoryEndpoint.GetById(categoryId);

            var account = new AccountModel(model.Name, model.Password, model.WebAdress, category);
            await _accountEndpoint.InsertAsync(account);

            AccountsList.Add(account);
        }

        public async Task DeleteAccount(AccountModel account)
        {
            await _accountEndpoint.DeleteAsync(account.Id);
            AccountsList.Remove(account);
        }

        public async Task UpdateAccount(AccountModel account)
        {
            await _accountEndpoint.UpdateAsync(account);
        }
    }
}