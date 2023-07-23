using Application.CommandQueries.Language.Commands.UpdateLanguage;
using Application.Common.Models;
using Application.Operations.Categories.Commands.CreateCategory;
using Application.Operations.Categories.Commands.UpdateCategory;
using Application.Operations.Categories.Queries.GetCategoryById;
using Application.Operations.CategoryTranslations.Commands.UpdateCategoryTranslation;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.categories
{
    public class AddOrEditModel : PageModel
    {
        [BindProperty]
        public CategoryDto? Category { get; set; }

        [BindProperty]
        public CategoryTranslationDto? Translation { get; set; }

        private readonly IMediator _mediator;

        public AddOrEditModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync(short id)
        {
            Category = await _mediator.Send(new GetCategoryByIdQuery() { Id = id });
        }

        public async Task<ActionResult> OnPostAsync(short id)
        {
            UpdateCategoryTranslationCommand translationCommand = new()
            {
                Id = Translation.Id,
                Title = Translation?.Title,
                Status = Translation.Status,
                InsertDate = Translation.InsertDate,
                PublishDate = Translation.PublishDate,
                LanguageId = Translation.LanguageId,
                CategoryId = Translation.CategoryId
            };

            return RedirectToPage("/admin/categories/addoredit", new { id });
        }
    }
}
