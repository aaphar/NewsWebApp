using Application.CommandQueries.Language.Commands.CreateLanguage;
using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.CategoryTranslations.Commands.CreateCategoryTranslation;
using Application.Operations.CategoryTranslations.Commands.UpdateCategoryTranslation;
using Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationByCategoryId;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace WebUI.Pages.admin.categories
{
    public class AddOrEditModel : PageModel
    {
        [BindProperty]
        public List<CategoryTranslationDto>? Translations { get; set; }

        [BindProperty]
        public CategoryTranslationDto? TranslationDto { get; set; }

        [BindProperty]
        public List<LanguageDto>? Languages { get; set; }



        private readonly IMediator _mediator;

        private readonly IValidator<UpdateCategoryTranslationCommand> _validator;
        private readonly IValidator<CreateCategoryTranslationCommand> _createValidator;

        public AddOrEditModel(
            IMediator mediator,
             IValidator<UpdateCategoryTranslationCommand> validator,
             IValidator<CreateCategoryTranslationCommand> createValidator)
        {
            _mediator = mediator;
            _validator = validator;
            _createValidator = createValidator;
        }

        public async Task OnGetAsync(short id)
        {
            Languages = await _mediator.Send(new GetLanguagesQuery());

            Translations = await _mediator.Send(new GetCategoryTranslationsByCategoryIdQuery() { CategoryId = id });

        }

        public async Task<ActionResult> OnPostAsync(short id)
        {
            UpdateCategoryTranslationCommand updateTranslation = new()
            {
                Title = TranslationDto?.Title?.Substring(0, 1).ToUpper() + TranslationDto?.Title?.Substring(1).ToLower(),
                //Status = TranslationDto.Status,
                Status = TranslationDto?.Status ?? Status.Draft,
                PublishDate = TranslationDto?.PublishDate ?? DateTime.Now ,
                CategoryId = id,
                LanguageId = TranslationDto?.LanguageId ?? null,
            };

            ValidationResult result = await _validator.ValidateAsync(updateTranslation);

            await _mediator.Send(updateTranslation);

            return RedirectToPage("/admin/categories/AddOrEdit", new { Id = id });

        }
    }
}
