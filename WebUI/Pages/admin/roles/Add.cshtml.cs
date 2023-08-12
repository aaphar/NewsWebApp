using Application.Operations.Roles.Commands.CreateRole;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.roles
{
    [Authorize(Roles = "Admin")]
    public class AddModel : PageModel
    {
        private readonly IMediator _mediator;

        private readonly IValidator<CreateRoleCommand> _validator;

        [BindProperty]
        public string? Title { get; init; }

        public AddModel(
        IMediator mediator,
        IValidator<CreateRoleCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }
        public async Task<ActionResult> OnPostAsync()
        {
            CreateRoleCommand createRoleCommand = new()
            {
                Name = Title.Substring(0, 1).ToUpper() + Title.Substring(1).ToLower(),
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
