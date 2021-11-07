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

        [BindProperty] public AccountModel SelectedAccount { get; set; }
        [BindProperty] public List<SelectListItem> CategorySelectItems { get; set; }
        [BindProperty] public string SelectedCategory { get; set; }

        public AccountsOverviewPageModel(MainViewModel mainVM)
        {
            MainVM = mainVM;
        }

        public void OnGet()
        {
            InitializeCategoryOptions();
        }

        private void InitializeCategoryOptions()
        {
            CategorySelectItems = new List<SelectListItem>();
            foreach (var c in MainVM.CategoriesList)
            {
                CategorySelectItems.Add(new SelectListItem(c.Name, c.Id.ToString()));
            }
        }

        public async Task<IActionResult> OnPostSaveEntry()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Guid.TryParse(SelectedCategory, out Guid guidCategory);
            await MainVM.CreateNewAccount(SelectedAccount, guidCategory);

            return RedirectToPage("/Accounts/AccountsOverviewPage");
        }

        public async Task<IActionResult> OnPostUpdate(int counter)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/Accounts/AccountsOverviewPage");
        }

        public async Task<IActionResult> OnPostDelete(int counter)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/Accounts/AccountsOverviewPage");
        }
    }
}