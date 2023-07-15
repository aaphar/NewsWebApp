using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Language.Queries.GetLanguageById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.languages
{
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
    }
}
