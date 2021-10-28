using KeySecret.App.Library.DataAccess;
using KeySecret.App.Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace KeySecret.App.Web.Pages.Authentication
{
    public class RegisterPageModel : PageModel
    {
        private readonly ILogger<RegisterPageModel> _logger;
        private readonly IAuthenticateEndpoint _authenticateEndpoint;

        [BindProperty]
        public RegisterModel RegisterCredencials { get; set; }
        public string UserMessage { get; set; }

        public RegisterPageModel(ILogger<RegisterPageModel> logger, IAuthenticateEndpoint authenticateEndpoint)
        {
            _logger = logger;
            _authenticateEndpoint = authenticateEndpoint;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid");
                return Page();
            }

            var response = await _authenticateEndpoint.Register(
                RegisterCredencials.Username,
                RegisterCredencials.Email,
                RegisterCredencials.Password);

            UserMessage = response.Message;

            return RedirectToPage("/Authentication/Login");
        }
    }
}