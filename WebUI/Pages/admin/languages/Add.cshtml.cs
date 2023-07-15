using Application.CommandQueries.Language.Commands.CreateLanguage;
using Application.Common.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.languages
{
    public class AddModel : PageModel
    {
        private readonly IMediator _mediator;

        private readonly IValidator<CreateLanguageCommand> _validator;

        [BindProperty]
        public string? Title { get; init; }
        [BindProperty]
        public string? Code { get; init; }

        public AddModel(
            IMediator mediator,
            IValidator<CreateLanguageCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        public async Task<ActionResult> OnPostAsync()
        {
            CreateLanguageCommand createLanguageCommand = new()
            {
                Title = Title,
                Code = Code,
            };

            ValidationResult result = await _validator.ValidateAsync(createLanguageCommand);

            if (!result.IsValid)
            {
                //result.AddToModelState(this.ModelState);

                return Page();
            }


            short id = await _mediator.Send(createLanguageCommand);
            string _message = $"Language with ID = {id} was successfully created";
            await Console.Out.WriteLineAsync(_message);

            return RedirectToPage("/admin/languages/detail", new { id });
        }
    }
}
