using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.posts.translation
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
