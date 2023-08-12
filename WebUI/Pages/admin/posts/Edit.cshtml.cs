using Application.Common.Models;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.Posts.Commands.UpdatePost;
using Application.Operations.Posts.Queries.GetPostById;
using Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageCodeAndNewsId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.posts
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public PostDto? Post { get; set; }

        [BindProperty]
        public string ImagePath { get; set; }
        public List<CategoryDto>? Categories { get; set; }

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task OnGetAsync(long id)
        {
            Post = await _mediator.Send(new GetPostByIdQuery(id));
            Categories = await _mediator.Send(new GetCategoriesQuery());

            if (Post != null)
            {
                ImagePath = Post.ImagePath;
            }
        }

        public async Task<ActionResult> OnPostAsync(long id)
        {
            // Retrieve the stored image URL from TempData
            var uploadedImagePath = TempData["UploadedImagePath"] as string;

            // If a new image was uploaded, update the ImagePath property
            if (!string.IsNullOrEmpty(uploadedImagePath))
            {
                // Update the ImagePath property with the uploaded image URL
                Post.ImagePath = uploadedImagePath;
            }
            
            // If no new image was uploaded, keep the existing ImagePath value
            else if (string.IsNullOrEmpty(Post.ImagePath))
            {
                Post.ImagePath = ImagePath;
            }

            UpdatePostCommand updatePostCommand = new()
            {
                Id = id,
                Title = Post?.Title,
                ImagePath = Post?.ImagePath,
                PublishDate = Post?.PublishDate,
                CategoryId = Post?.CategoryId
            };

            TempData["UploadedImagePath"] = null;

            await _mediator.Send(updatePostCommand);
            string _message = $"Role with ID = {id} was successfully updated";
            await Console.Out.WriteLineAsync(_message);

            return RedirectToPage("/admin/posts/index");
        }
    }
}
