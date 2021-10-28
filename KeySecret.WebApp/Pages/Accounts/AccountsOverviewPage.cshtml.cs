using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace KeySecret.App.Web.Pages.Accounts
{
    public class AccountsOverviewPageModel : PageModel
    {
        public readonly MainViewModel MainVM;

        public AccountsOverviewPageModel(MainViewModel mainVM)
        {
            MainVM = mainVM;
        }

        public async Task OnGet()
        {
            await MainVM.InitializeAccountsList();
        }
    }
}