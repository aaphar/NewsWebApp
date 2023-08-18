using Application.CommandQueries.Language.Commands.CreateLanguage;
using Application.Operations.Hashtags.Commands.CreateHashtag;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.hashtags;

[Authorize(Roles = "Adminstrator")]
public class AddModel : PageModel
{
    private readonly IMediator _mediator;

    private readonly IValidator<CreateHashtagCommand> _validator;

    [BindProperty]
    public string? Title { get; init; }

    public AddModel(
            IMediator mediator,
            IValidator<CreateHashtagCommand> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<ActionResult> OnPostAsync()
    {
        CreateHashtagCommand createHashtagCommand = new()
        {
            Title = Title,
        };

        ValidationResult result = await _validator.ValidateAsync(createHashtagCommand);

        if (!result.IsValid)
        {
            return Page();
        }

        long id = await _mediator.Send(createHashtagCommand);

        return RedirectToPage("/admin/hashtags/detail", new { id });
    }
}
