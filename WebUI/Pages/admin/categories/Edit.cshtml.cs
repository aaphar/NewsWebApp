using Application.Common.Models;
using Application.Operations.Categories.Commands.UpdateCategory;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.Categories.Queries.GetCategoryById;
using Application.Operations.Posts.Commands.UpdatePost;
using Application.Operations.Posts.Queries.GetPostById;
using Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageCodeAndNewsId;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.categories
{
    [Authorize(Roles = "Adminstrator")]
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public CategoryDto? Category { get; set; }

        public long AuthorId { get; set; }

        private readonly UserManager<User> _userManager; // Add UserManager

        public EditModel(IMediator mediator,
             UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task OnGetAsync(short id)
        {
            Category = await _mediator.Send(new GetCategoryByIdQuery() { Id = id });

            // Get the currently logged-in user's Id
            var loggedInUserId = _userManager.GetUserId(User);
            AuthorId = int.Parse(loggedInUserId); // Convert to int if needed
        }

        public async Task<ActionResult> OnPostAsync(short id)
        {
            UpdateCategoryCommand updatePostCommand = new()
            {
                Id = id,
                Description = Category.Description,
                AuthorId = AuthorId,
            };


            await _mediator.Send(updatePostCommand);

            return RedirectToPage("/admin/categories/index");
        }
    }
}
