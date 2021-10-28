using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeySecret.App.Web.Pages
{
    public class IndexModel : PageModel
    {
        public MainViewModel MainVM { get; set; }

        public IndexModel(MainViewModel mainVM)
        {
            MainVM = mainVM;
        }
    }
}