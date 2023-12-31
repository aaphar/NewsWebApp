using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages
{
    public class AboutModel : PageModel
    {
		private readonly IMediator _mediator;

		public List<LanguageDto>? Languages { get; set; }

		[BindProperty]
		public string? SelectedLanguageCode { get; set; }

		public AboutModel(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task OnGet()
		{
			Languages = await _mediator.Send(new GetLanguagesQuery());

			// Get the language code from the URL path
			var pathSegments = HttpContext.Request.Path.Value.Split('/');
			var languageFromUrl = pathSegments.Length > 2 ? pathSegments[1] : null;

			var languageFromSession = HttpContext.Session.GetString("SelectedLanguage");

			var language = !string.IsNullOrEmpty(languageFromUrl) ? languageFromUrl : languageFromSession;

			SelectedLanguageCode = language;
		}
	}
}
