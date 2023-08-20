using Application.Common.Models;
using Application.Operations.Hashtags.Commands.UpdateHashtag;
using Application.Operations.Hashtags.Queries.GetHashtagById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.hashtags;

[Authorize(Roles = "Adminstrator")]
public class EditModel : PageModel
{
    private readonly IMediator _mediator;

    [BindProperty]
    public HashtagDto? Hashtag { get; set; }

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
        Hashtag = await _mediator.Send(new GetHashtagByIdQuery(id));   // burda id=0;
                                                                       // Get the currently logged-in user's Id
        var loggedInUserId = _userManager.GetUserId(User);
        AuthorId = int.Parse(loggedInUserId); // Convert to int if needed
    }

    public async Task<ActionResult> OnPostAsync(long id)
    {
        UpdateHashtagCommand updateHashtag = new()
        {
            Id = id,
            Title = Hashtag?.Title,
            AuthorId=AuthorId
        };


        await _mediator.Send(updateHashtag);

        return RedirectToPage("/admin/hashtags/detail", new { id });
    }

}
