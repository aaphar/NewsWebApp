using Application.Common.Models;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.Posts.Commands.UpdatePost;
using Application.Operations.Posts.Queries.GetPostById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.posts
{
    [Authorize(Roles = "Adminstrator")]
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public PostDto? Post { get; set; }

        [BindProperty]
        public string ImagePath { get; set; }
        public List<CategoryDto>? Categories { get; set; }

        public long AuthorId { get; set; }

        private readonly UserManager<User> _userManager; // Add UserManager

        public EditModel(IMediator mediator,
            UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }
        public async Task OnGetAsync(long id)
        {
            Post = await _mediator.Send(new GetPostByIdQuery(id));
            Categories = await _mediator.Send(new GetCategoriesQuery());

            if (Post != null)
            {
                ImagePath = Post.ImagePath;
            }

            // Get the currently logged-in user's Id
            var loggedInUserId = _userManager.GetUserId(User);
            AuthorId = int.Parse(loggedInUserId); // Convert to int if needed

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
                CategoryId = Post?.CategoryId,

                AuthorId = AuthorId,
            };

            TempData["UploadedImagePath"] = null;

            await _mediator.Send(updatePostCommand);
            string _message = $"Role with ID = {id} was successfully updated";
            await Console.Out.WriteLineAsync(_message);

            return RedirectToPage("/admin/posts/index");
        }
    }
}
