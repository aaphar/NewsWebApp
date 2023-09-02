using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.Categories.Queries.GetCategoryByTitle;
using Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationByLanguageCodeAndCategoryI;
using Application.Operations.Posts.Queries.GetPostsByCategoryId;
using Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageCodeAndNewsId;
using Application.Operations.Users.Queries.GetUserById;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages
{
	public class CategoryModel : PageModel
	{
		private readonly IMediator _mediator;

		public List<LanguageDto>? Languages { get; set; }

		[BindProperty]
		public string? SelectedLanguageCode { get; set; }

		public CategoryDto? Category { get; set; }


		public List<PostDto>? Posts { get; set; }

		public List<CategoryDto>? Categories { get; set; }


		public int CurrentPageIndex { get; set; }

		public int TotalPostCount { get; set; }


		public CategoryModel(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task OnGet(string title, int pageIndex = 1)
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

			Category = await _mediator.Send(new GetCategoryByTitleQuery(title));

			if (Category != null)
			{
				try
				{
					Category.CategoryTranslation = await _mediator.Send(new GetCategoryTranslationByLanguageCodeAndCategoryIdQuery()
					{
						LanguageCode = language,
						CategoryId = Category.Id
					});
				}
				catch (CategoryNotFoundWithGivenLanguageException)
				{
					Category.CategoryTranslation = new CategoryTranslationDto { ErrorMessage = "Translation not found." };
				}
				catch (Exception)
				{
					Category.CategoryTranslation = new CategoryTranslationDto { ErrorMessage = "An error occurred while fetching translation." };
				}

				Posts = await _mediator.Send(new GetPostsByCategoryIdQuery(Category.Id));

				if (Posts != null)
				{
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
				}

				TotalPostCount = Posts.Count;
				var pageSize = 5; // You can adjust the page size as needed
				var itemsToSkip = (pageIndex - 1) * pageSize;

				// Fetch posts for the current page
				Posts = Posts.Skip(itemsToSkip).Take(pageSize).ToList();
				// Set the current page index for pagination links
				CurrentPageIndex = pageIndex;
			}
		}


		public Task<IActionResult> OnPostAsync()
		{
			// Reload the page with the selected LanguageId
			return Task.FromResult<IActionResult>(RedirectToPage("/category", new { pageIndex = 1 }));
		}
	}
}