using Application.CommandQueries.Language.Commands.DeleteLanguage;
using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.language
{
    [Authorize(Roles = "Adminstrator")]
    public class LanguagesModel : PageModel
    {
        private readonly IMediator _mediator;

        public List<LanguageDto>? Languages { get; set; }


        public LanguagesModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync()
        {
            Languages = await _mediator.Send(new GetLanguagesQuery());
        }

        public async Task<IActionResult> OnPostDeleteAsync(short Id)
        {
            await _mediator.Send(new DeleteLanguageCommand(Id));

            return RedirectToPage("/admin/languages/index");
        }
    }
}
