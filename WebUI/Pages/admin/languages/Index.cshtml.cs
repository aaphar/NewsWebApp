using Application.CommandQueries.Language.Commands.CreateLanguage;
using Application.CommandQueries.Language.Commands.DeleteLanguage;
using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.language
{
    public class LanguagesModel : PageModel
    {
        private readonly IMediator _mediator;

        private readonly IValidator<CreateLanguageCommand> _validator;

        [BindProperty]
        public string? Title { get; init; }
        [BindProperty]
        public string? Code { get; init; }
        public List<LanguageDto>? Languages { get; set; }

        public LanguageDto? LanguageDto { get; set; }


        public LanguagesModel(
            IMediator mediator,
            IValidator<CreateLanguageCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        public async Task OnGetAsync()
        {
            Languages = await _mediator.Send(new GetLanguagesQuery());
        }

        public async Task<IActionResult> OnPostDeleteAsync(short Id)
        {
            await _mediator.Send(new DeleteLanguageCommand(Id));

            string _message = $"Language with Id = {Id} was successfully deleted";

            await Console.Out.WriteLineAsync(_message);

            return RedirectToPage("/admin/languages/index"); 
        }
    }
}
