using Application.Operations.Roles.Commands.CreateRole;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.roles
{
    [Authorize(Roles = "Adminstrator")]
    public class AddModel : PageModel
    {
        private readonly IMediator _mediator;

        private readonly IValidator<CreateRoleCommand> _validator;

        [BindProperty]
        public string? Title { get; init; }


        public long AuthorId { get; set; }

        private readonly UserManager<User> _userManager; // Add UserManager


        public AddModel(
        IMediator mediator,
        IValidator<CreateRoleCommand> validator,
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
            CreateRoleCommand createRoleCommand = new()
            {
                Name = Title,
                AuthorId = AuthorId
            };

            ValidationResult result = await _validator.ValidateAsync(createRoleCommand);

            if (!result.IsValid)
            {
                return Page();
            }

            int id = await _mediator.Send(createRoleCommand);
            string _message = $"Role with ID = {id} was successfully created";
            await Console.Out.WriteLineAsync(_message);

            return RedirectToPage("/admin/roles/index");
        }
    }
}
