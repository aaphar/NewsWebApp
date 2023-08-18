using Application.CommandQueries.Language.Commands.DeleteLanguage;
using Application.Common.Models;
using Application.Operations.Language.Queries.GetLanguageById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.languages
{
    [Authorize(Roles = "Adminstrator")]
    public class DetailModel : PageModel
    {
        private readonly IMediator _mediator;

        public LanguageDto? Language { get; set; }

        public DetailModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync(short id)
        {

            GetLanguageByIdQuery getLanguageByIdQuery = new()
            {
                Id = id
            };

            Language = await _mediator.Send(getLanguageByIdQuery);
        }

        public async Task<IActionResult> OnPostDeleteAsync(short Id)
        {
            await _mediator.Send(new DeleteLanguageCommand(Id));

            return RedirectToPage("/admin/languages/index");
        }
    }
}
