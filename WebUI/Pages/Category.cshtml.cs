using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages
{
    public class CategoryModel : PageModel
    {
        public List<PostDto> Posts { get; set; }

        //public Task OnGet(short categoryId)
        //{

        //}
    }
}
