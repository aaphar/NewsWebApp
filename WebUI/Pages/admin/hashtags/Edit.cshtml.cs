using Application.Common.Models;
using Application.Operations.Hashtags.Commands.UpdateHashtag;
using Application.Operations.Hashtags.Queries.GetHashtagById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.hashtags;

[Authorize(Roles = "Adminstrator")]
public class EditModel : PageModel
{
    private readonly IMediator _mediator;

    [BindProperty]
    public HashtagDto? Hashtag { get; set; }

    public EditModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task OnGetAsync(long id)
    {
        Hashtag = await _mediator.Send(new GetHashtagByIdQuery(id));   // burda id=0;
    }

    public async Task<ActionResult> OnPostAsync(long id)
    {
        UpdateHashtagCommand updateHashtag = new()
        {
            Id = id,
            Title = Hashtag?.Title,
        };


        await _mediator.Send(updateHashtag);

        return RedirectToPage("/admin/hashtags/detail", new { id });
    }

}
