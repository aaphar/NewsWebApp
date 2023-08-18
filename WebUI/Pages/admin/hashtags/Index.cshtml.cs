using Application.Common.Models;
using Application.Operations.Hashtags.Commands.DeleteHashtag;
using Application.Operations.Hashtags.Queries.GetHashtags;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.hashtags;

[Authorize(Roles = "Adminstrator")]
public class HashtagModel : PageModel
{
    private readonly IMediator _mediator;

    public List<HashtagDto>? Hashtags { get; set; }

    public HashtagModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task OnGetAsync()
    {
        Hashtags = await _mediator.Send(new GetHashtagsQuery());
    }

    public async Task<IActionResult> OnPostDeleteAsync(long Id)
    {
        await _mediator.Send(new DeleteHashtagCommand(Id));             

        return RedirectToPage("/admin/hashtags/index");
    }
}
