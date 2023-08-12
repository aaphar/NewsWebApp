using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Posts.Commands.DeletePost;
using Application.Operations.Posts.Queries.GetPostById;
using Application.Operations.Posts.Queries.GetPosts;
using Application.Operations.PostTranslations.Commands.DeletePostTranslation;
using Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageIdAndNewsId;
using Application.Operations.PostTranslations.Queries.GetPostTranslationsByNewsId;
using Application.Operations.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.posts;

[Authorize(Roles = "Admin")]
public class DetailModel : PageModel
{
    private readonly IMediator _mediator;

    public PostDto? Post { get; set; }

    [BindProperty]
    public short LanguageId { get; set; }

    [BindProperty]
    public long PostId { get; set; }

    public List<PostTranslationDto>? PostTranslations { get; set; }

    public List<LanguageDto>? Languages { get; set; }

    public List<PostDto>? Posts { get; set; }

    public List<UserDto> Users { get; set; }

    public DetailModel(IMediator mediator)
    {
        _mediator = mediator;
        PostTranslations = new List<PostTranslationDto>();
    }

    public async Task OnGetAsync(long id, short languageId)
    {
        // get post by id
        Post = await _mediator.Send(new GetPostByIdQuery(id));

        Posts = await _mediator.Send(new GetPostsQuery());

        //get languages
        Languages = await _mediator.Send(new GetLanguagesQuery());

        Users = await _mediator.Send(new GetUsersQuery());


        if (languageId != 0)
        {
            try
            {
                var translation = await _mediator.Send(new GetPostTranslationByLanguageIdAndNewsIdQuery() { LanguageId = languageId, NewsId = id });

                if (translation != null)
                {
                    PostTranslations.Add(translation);
                }
                else
                {
                    PostTranslations = null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while fetching the category translation: {e}");

                // Display a user-friendly message
                PostTranslations = null;
                ModelState.AddModelError(string.Empty, "An error occurred while fetching the category translation. Please try again later.");
            }
        }
        else
        {
            PostTranslations = await _mediator.Send(new GetPostTranslationsByNewsIdQuery { NewsId = id });
        }
    }

    public Task<IActionResult> OnPostAsync(long id)
    {
        // Reload the page with the selected LanguageId
        return Task.FromResult<IActionResult>(RedirectToPage("/admin/posts/detail", new { Id = id, LanguageId }));
    }

    public async Task<IActionResult> OnPostDeleteAsync(long Id)
    {
        await _mediator.Send(new DeletePostTranslationCommand(Id));

        string _message = $"Post with Id = {Id} was successfully deleted";

        await Console.Out.WriteLineAsync(_message);


        // redirect problem
        return RedirectToPage("/admin/posts/detail", new { Id = PostId });
    }
}


