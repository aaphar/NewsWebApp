using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.Posts.Queries.GetPosts;
using Application.Operations.Posts.Queries.GetPostsByCategoryId;
using Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageCodeAndNewsId;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages
{
    public class BlogModel : PageModel
    {
        private readonly IMediator _mediator;

        public List<LanguageDto>? Languages { get; set; }

        [BindProperty]
        public string? SelectedLanguageCode { get; set; }

        public List<PostDto>? Posts { get; set; }

        [BindProperty]
        public long CurrentPostId { get; set; }

        public PostTranslationDto? PostTranslation { get; set; }

        public List<CategoryDto>? Categories { get; set; }

        public int CurrentPageIndex { get; set; }

        public int TotalPostCount { get; set; }

        public BlogModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync(int pageIndex = 1)
        {
            Languages = await _mediator.Send(new GetLanguagesQuery());

            // Get the language code from the URL path
            var pathSegments = HttpContext.Request.Path.Value.Split('/');
            var languageFromUrl = pathSegments.Length > 2 ? pathSegments[1] : null;

            var languageFromSession = HttpContext.Session.GetString("SelectedLanguage");

            var language = !string.IsNullOrEmpty(languageFromUrl) ? languageFromUrl : languageFromSession;

            SelectedLanguageCode = language;

            if (Languages != null && Languages.Count > 0)
            {
                if (language == null)
                {
                    SelectedLanguageCode = Languages[0].LanguageCode; // Set the default selected language
                    language = SelectedLanguageCode; // Set the language to the default code
                }
            }

            Categories = await _mediator.Send(new GetCategoriesQuery());

            for (int i = 0; i < Categories.Count; i++)
            {
                Categories[i].Posts = await _mediator.Send(new GetPostsByCategoryIdQuery(Categories[i].Id));
            }

            Posts = await _mediator.Send(new GetPostsQuery());

            // Fetch post translations for each post
            for (int i = Posts.Count - 1; i >= 0; i--)
            {
                var post = Posts[i];
                try
                {
                    post.PostTranslation = await _mediator.Send(new GetPostTranslationByLanguageCodeAndNewsIdQuery
                    {
                        LanguageCode = language != null ? language : SelectedLanguageCode,
                        NewsId = post.Id
                    });
                }
                catch (Exception e)
                {
                    Posts.RemoveAt(i);
                }
            }

            TotalPostCount = Posts.Count;
            var pageSize = 5; // You can adjust the page size as needed
            var itemsToSkip = (pageIndex - 1) * pageSize;

            // Fetch posts for the current page
            Posts = Posts.Skip(itemsToSkip).Take(pageSize).ToList();
            // Set the current page index for pagination links
            CurrentPageIndex = pageIndex;
        }

        public Task<IActionResult> OnPostAsync()
        {
            // Reload the page with the selected LanguageId
            return Task.FromResult<IActionResult>(RedirectToPage("/blog", new { pageIndex = 1 }));
        }
    }
}
