using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.Posts.Queries.GetPosts;
using Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageIdAndNewsId;
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
        public short SelectedLanguageId { get; set; }

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

        public async Task OnGetAsync(short language, int pageIndex = 1)
        {
            SelectedLanguageId = language;
            Categories = await _mediator.Send(new GetCategoriesQuery());

            Languages = await _mediator.Send(new GetLanguagesQuery());

            if (Languages != null && Languages.Count > 0)
            {
                if (language == 0)
                {
                    SelectedLanguageId = Languages[0].Id; // Set the default selected language            
                }
            }

            Posts = await _mediator.Send(new GetPostsQuery());
                        
            // Fetch post translations for each post
            for (int i = Posts.Count - 1; i >= 0; i--)
            {
                var post = Posts[i];
                try
                {
                    post.PostTranslation = await _mediator.Send(new GetPostTranslationByLanguageIdAndNewsIdQuery
                    {
                        LanguageId = language != 0 ? language : SelectedLanguageId,
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
            return Task.FromResult<IActionResult>(RedirectToPage("/blog", new { language = SelectedLanguageId, pageIndex = 1 }));
        }
    }
}
