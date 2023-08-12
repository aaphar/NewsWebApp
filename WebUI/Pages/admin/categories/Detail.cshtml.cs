using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.Categories.Queries.GetCategoryById;
using Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationByCategoryId;
using Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationsByLanguageId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.categories;

[Authorize(Roles = "Admin")]
public class DetailModel : PageModel
{
    private readonly IMediator _mediator;

    public CategoryDto? Category { get; set; }

    [BindProperty]
    public short LanguageId { get; set; }

    public List<CategoryTranslationDto>? CategoryTranslations { get; set; }

    public List<LanguageDto>? Languages { get; set; }

    public List<CategoryDto>? Categories { get; set; }

    public DetailModel(IMediator mediator)
    {
        _mediator = mediator;
        CategoryTranslations = new List<CategoryTranslationDto>();
    }

    public async Task OnGetAsync(short id, short languageId)
    {
        // get category by id
        Category = await _mediator.Send(new GetCategoryByIdQuery { Id = id });

        Categories = await _mediator.Send(new GetCategoriesQuery());

        // get languages
        Languages = await _mediator.Send(new GetLanguagesQuery());


        if (languageId != 0)
        {
            try
            {
                var translation = await _mediator.Send(new GetCategoryTranslationByLanguageAndCategoryIdQuery { LanguageId = languageId, CategoryId = id });

                if (translation != null)
                {
                    CategoryTranslations.Add(translation);
                }
                else
                {
                    CategoryTranslations = null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while fetching the category translation: {e}");

                // Display a user-friendly message
                CategoryTranslations = null;
                ModelState.AddModelError(string.Empty, "An error occurred while fetching the category translation. Please try again later.");
            }
        }
        else
        {
            CategoryTranslations = await _mediator.Send(new GetCategoryTranslationsByCategoryIdQuery { CategoryId = id });
        }
    }

    public Task<IActionResult> OnPostAsync(short id)
    {
        // Reload the page with the selected LanguageId
        return Task.FromResult<IActionResult>(RedirectToPage("/admin/categories/detail", new { Id = id, LanguageId }));
    }
}

