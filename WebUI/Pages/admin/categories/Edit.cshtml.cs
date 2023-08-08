using Application.Common.Models;
using Application.Operations.Categories.Commands.UpdateCategory;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.Categories.Queries.GetCategoryById;
using Application.Operations.Posts.Commands.UpdatePost;
using Application.Operations.Posts.Queries.GetPostById;
using Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageCodeAndNewsId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.categories
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public CategoryDto? Category { get; set; }

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task OnGetAsync(short id)
        {
            Category = await _mediator.Send(new GetCategoryByIdQuery() { Id = id });
        }

        public async Task<ActionResult> OnPostAsync(short id)
        {            
            UpdateCategoryCommand updatePostCommand = new()
            {
                Id = id,
                Description=Category.Description,
            };


            await _mediator.Send(updatePostCommand);
            
            return RedirectToPage("/admin/categories/index");
        }
    }
}
