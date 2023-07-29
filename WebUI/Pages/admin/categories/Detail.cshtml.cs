using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.Categories.Queries.GetCategoryById;
using Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationByCategoryId;
using Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationById;
using Application.Operations.CategoryTranslations.Queries.GetCategoryTranslations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.categories;
public class DetailModel : PageModel
{
    private readonly IMediator _mediator;

    public CategoryDto? Category { get; set; }

    public short LanguageId { get; set; }

    public List<CategoryTranslationDto>? CategoryTranslations { get; set; }

    public List<LanguageDto>? Languages { get; set; }

    public List<CategoryDto>? Categories { get; set; }

    public DetailModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task OnGetAsync(short id)
    {
        // get category by id
        Category = await _mediator.Send(new GetCategoryByIdQuery() { Id = id }); 

        Categories= await _mediator.Send(new GetCategoriesQuery());

        //get languages
        Languages = await _mediator.Send(new GetLanguagesQuery());

        // get translations
        CategoryTranslations = await _mediator.Send(new GetCategoryTranslationsByCategoryIdQuery() { CategoryId = id });
        
    }
}

