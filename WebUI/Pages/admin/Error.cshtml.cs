using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin
{
    [AllowAnonymous]
    public class ErrorModel : PageModel
    {
        public string ErrorMessage { get; private set; }

        public void OnGet()
        {
            ErrorMessage = TempData["ErrorMessage"] as string;
        }
    }
}
