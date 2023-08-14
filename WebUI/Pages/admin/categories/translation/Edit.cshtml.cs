using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.categories.translation
{
    [Authorize(Roles = "Adminstrator")]
    public class EditModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
