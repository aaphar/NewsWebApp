using Application.CommandQueries.Language.Commands.UpdateLanguage;
using Application.Common.Models;
using Application.Operations.Language.Queries.GetLanguageById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.language
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        public LanguageDto? LanguageDto { get; set; }

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync(short id)
        {
            LanguageDto = await _mediator.Send(new GetLanguageByIdQuery() { Id = id }); ;
        }

        public async Task<ActionResult> OnPutAsync()
        {
            UpdateLanguageCommand updateLanguageCommand = new()
            {
                Title = LanguageDto?.Title,
                Code = LanguageDto?.LanguageCode
            };


            short id = await _mediator.Send(updateLanguageCommand);
            string _message = $"Language with ID = {id} was successfully created";
            await Console.Out.WriteLineAsync(_message);

            return RedirectToPage("/admin/languages/detail", new { LanguageDto?.Id });
        }

    }
}
