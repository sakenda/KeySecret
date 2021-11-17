using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using KeySecret.App.Library.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KeySecret.App.Web.Pages.Accounts
{
    public class AccountsOverviewPageModel : PageModel
    {
        public MainViewModel MainVM { get; }

        [BindProperty] public AccountModelSelected SelectedAccount { get; set; }
        [BindProperty] public string NewCategoryName { get; set; }
        [BindProperty] public List<SelectListItem> CategorySelectItems { get; set; }
        [BindProperty] public string SelectedCategory { get; set; }

        public AccountsOverviewPageModel(MainViewModel mainVM)
        {
            MainVM = mainVM;
        }

        public void OnGet()
        {
            InitializeCategoryOptions();
            SelectedAccount = null;
        }

        private void InitializeCategoryOptions()
        {
            CategorySelectItems = new List<SelectListItem>();
            foreach (var c in MainVM.CategoriesList)
            {
                CategorySelectItems.Add(new SelectListItem(c.Name, c.Id.ToString()));
            }
        }

        public async Task<IActionResult> OnPostAdd()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await MainVM.CreateNewAccount(SelectedAccount);
            SelectedAccount = null;

            return RedirectToPage("/Accounts/AccountsOverviewPage");
        }

        public async Task<IActionResult> OnPostUpdate(Guid accountId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SelectedAccount.Id = accountId;

            await MainVM.UpdateAccount(SelectedAccount);
            SelectedAccount = null;

            return RedirectToPage("/Accounts/AccountsOverviewPage");
        }

        public async Task<IActionResult> OnPostDelete(Guid accountId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SelectedAccount.Id = accountId;

            await MainVM.DeleteAccount(SelectedAccount);
            SelectedAccount = null;

            return RedirectToPage("/Accounts/AccountsOverviewPage");
        }

        public async Task<IActionResult> OnPostAddCategory()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await MainVM.CreateNewCategory(NewCategoryName);
            NewCategoryName = null;

            return RedirectToPage("/Accounts/AccountsOverviewPage");
        }

    }
}