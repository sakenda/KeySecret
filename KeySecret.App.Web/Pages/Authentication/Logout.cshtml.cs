using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeySecret.App.Web.Pages.Authentication
{
    public class LogoutModel : PageModel
    {
        public MainViewModel MainVM { get; }

        public LogoutModel(MainViewModel vm)
        {
            MainVM = vm;
        }

        public IActionResult OnPostLogoutAsync()
        {
            MainVM.ApiHelper.LogOffUser();
            return RedirectToPage("Login");
        }

        public IActionResult OnPostDontLogoutAsync()
        {
            return RedirectToPage("Index");
        }
    }
}
