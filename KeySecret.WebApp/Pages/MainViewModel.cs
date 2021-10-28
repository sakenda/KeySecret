using KeySecret.App.Library;
using KeySecret.App.Library.Helper;
using KeySecret.App.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KeySecret.App.Web.Pages
{
    public class MainViewModel
    {
        private readonly ILogger<MainViewModel> _logger;
        private readonly IApiHelper _apiHelper;
        private readonly IEndpoint<AccountModel> _accountEndpoint;

        [BindProperty]
        public IEnumerable<AccountModel> AccountsList { get; set; }

        [BindProperty]
        public CurrentUser CurrentUser => _apiHelper.LoggedInUser;

        public MainViewModel(ILogger<MainViewModel> logger, IApiHelper apiHelper, IEndpoint<AccountModel> accountEndpoint)
        {
            _apiHelper = apiHelper;
            _accountEndpoint = accountEndpoint;
            _logger = logger;
        }

        public async Task InitializeAccountsList()
        {
            AccountsList = await _accountEndpoint.GetAllAsync();
        }
    }
}