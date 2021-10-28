using KeySecret.DataAccess.Library.Models;
using KeySecret.App.Library.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace KeySecret.App.Web.Pages.Authentication
{
    public class LoginPageModel : PageModel
    {
        private readonly IAuthenticateEndpoint _authenticateEndpoint;

        [BindProperty]
        public LoginModel LoginCredencials { get; set; }

        public LoginPageModel(IAuthenticateEndpoint authenticateEndpoint)
        {
            _authenticateEndpoint = authenticateEndpoint;
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            await _authenticateEndpoint.Authenticate(LoginCredencials.Username, LoginCredencials.Password);

            return RedirectToPage("/Accounts/AccountsOverviewPage");
        }
    }
}