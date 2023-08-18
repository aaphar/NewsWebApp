using Application.Common.Models;
using Application.Operations.Hashtags.Commands.DeleteHashtag;
using Application.Operations.Hashtags.Queries.GetHashtagById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.hashtags;

[Authorize(Roles = "Adminstrator")]
public class DetailModel : PageModel
{
    private readonly IMediator _mediator;

    public HashtagDto? Hashtag { get; set; }

    public DetailModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task OnGetAsync(long id)
    {
        Hashtag = await _mediator.Send(new GetHashtagByIdQuery(id));
    }

    public async Task<IActionResult> OnPostDeleteAsync(long Id)
    {
        await _mediator.Send(new DeleteHashtagCommand(Id));
                
        return RedirectToPage("/admin/hashtags/index");
    }
}
