using Application.Common.Models;
using Application.Operations.Posts.Commands.CreatePost;
using Application.Operations.Posts.Commands.DeletePost;
using Application.Operations.Posts.Queries.GetPosts;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.post
{
    public class PostModel : PageModel
    {
        private readonly IMediator _mediator;

        private readonly IValidator<CreatePostCommand> _validator;

        public PostModel(
            IMediator mediator,
            IValidator<CreatePostCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        public List<PostDto> Posts { get; set; }

        public async Task OnGetAsync()
        {
            Posts = await _mediator.Send(new GetPostsQuery());
        }

        public async Task<IActionResult> OnPostDeleteAsync(long Id)
        {
            await _mediator.Send(new DeletePostCommand(Id));

            string _message = $"Post with Id = {Id} was successfully deleted";

            await Console.Out.WriteLineAsync(_message);

            return RedirectToPage("/admin/posts/index");
        }
    }
}
