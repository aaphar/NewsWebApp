using Application.CommandQueries.Language.Commands.CreateLanguage;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.languages
{
    [Authorize(Roles = "Adminstrator")]
    public class AddModel : PageModel
    {
        private readonly IMediator _mediator;

        private readonly IValidator<CreateLanguageCommand> _validator;

        [BindProperty]
        public string? Title { get; init; }
        [BindProperty]
        public string? Code { get; init; }


        public long AuthorId { get; set; }

        private readonly UserManager<User> _userManager; // Add UserManager

        public AddModel(
            IMediator mediator,
            IValidator<CreateLanguageCommand> validator,
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
            CreateLanguageCommand createLanguageCommand = new()
            {
                Title = Title?.Substring(0, 1).ToUpper() + Title?.Substring(1).ToLower(),
                Code = Code?.ToLower(),
                AuthorId = AuthorId
            };

            ValidationResult result = await _validator.ValidateAsync(createLanguageCommand);

            if (!result.IsValid)
            {
                //result.AddToModelState(this.ModelState);

                return Page();
            }


            short id = await _mediator.Send(createLanguageCommand);
            
            return RedirectToPage("/admin/languages/detail", new { id });
        }
    }
}
