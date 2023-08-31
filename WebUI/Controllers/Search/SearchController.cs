using Application.Common.Models;
using Application.Operations.PostTranslations.Queries.GetPostSearchsByTitle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers.Search
{
    public class SearchController : Controller
    {
        private readonly IMediator _mediator;

        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchTerm)
        {
            // Use the searchTerm to search for PostTranslations
            List<PostTranslationDto> searchResults = await _mediator.Send(new GetPostTranslationsByTitleQuery(searchTerm));

            // Return the SearchResults Razor page with the search results as the model
            return View("~/Pages/Search-result.cshtml", searchResults);
        }
    }
}
