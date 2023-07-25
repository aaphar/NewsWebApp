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
        public string? TranslationTitle { get; set; }

        [BindProperty]
        public Status Status { get; set; }

        [BindProperty]
        public DateTime PublishDate { get; set; }

        [BindProperty]
        public short CategoryId { get; set; }

        [BindProperty]
        public short LanguageId { get; set; }



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
            Debug.WriteLine("This message will appear in the server-side debugging console.");

            Languages = await _mediator.Send(new GetLanguagesQuery());

            Translations = await _mediator.Send(new GetCategoryTranslationsByCategoryIdQuery() { CategoryId = id });

        }

        public async Task<ActionResult> OnPostAsync(short id)
        {
            UpdateCategoryTranslationCommand updateTranslation = new()
            {
                Title = TranslationDto.Title,
                Status = TranslationDto.Status,
                PublishDate = TranslationDto.PublishDate,
                CategoryId = id,
                LanguageId = TranslationDto.LanguageId,
            };

            ValidationResult result = await _validator.ValidateAsync(updateTranslation);

            await _mediator.Send(updateTranslation);

            return RedirectToPage("/admin/categories/AddOrEdit", new { Id = id });

        }
    }
}
