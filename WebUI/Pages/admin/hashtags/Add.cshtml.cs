using Application.CommandQueries.Language.Commands.CreateLanguage;
using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Operations.Hashtags.Commands.CreateHashtag;
using Application.Operations.Language.Queries.GetLanguageByCode;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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


    public long AuthorId { get; set; }

    private readonly UserManager<User> _userManager; // Add UserManager

    public AddModel(
            IMediator mediator,
            IValidator<CreateHashtagCommand> validator,
            UserManager<User> userManager)
    {
        _mediator = mediator;
        _validator = validator;
        _userManager = userManager;
    }

    public Task OnGetAsync()
    {
        // Get the currently logged-in user's Id
        var loggedInUserId = _userManager.GetUserId(User);
        AuthorId = int.Parse(loggedInUserId); // Convert to int if needed
        return Task.CompletedTask;
    }

    public async Task<ActionResult> OnPostAsync()
    {
        CreateHashtagCommand createHashtagCommand = new()
        {
            Title = Title,
            AuthorId = AuthorId
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
