using Application.CommandQueries.Language.Commands.CreateLanguage;
using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Categories.Commands.CreateCategory;
using Application.Operations.CategoryTranslations.Commands.CreateCategoryTranslation;
using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.categories
{
    public class AddModel : PageModel
    {
        // her bir dili goturmeliyem list landtos
        // langs list foreach her biri ucun post

        // category dto
        [BindProperty]
        public CategoryDto? Category { get; set; }

        // category translation
        public string? Title { get; set; }

        public Status Status { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime PublishDate { get; set; }

        public short LanguageId { get; set; }

        public short CategoryId { get; set; }

        [BindProperty]
        public List<CategoryTranslationDto> Translations { get; set; }

        // languages
        public List<LanguageDto>? Languages { get; set; }

        private readonly IMediator _mediator;

        private readonly IValidator<CreateCategoryCommand> _categoryValidator;

        private readonly IValidator<CreateCategoryTranslationCommand> _validator;

        public AddModel(
            IMediator mediator,
            IValidator<CreateCategoryCommand> categoryValidator,
            IValidator<CreateCategoryTranslationCommand> validator)
        {
            _mediator = mediator;
            _categoryValidator = categoryValidator;
            _validator = validator;

            Translations = new List<CategoryTranslationDto>();
        }

        public async Task OnGetAsync()
        {
            Languages = await _mediator.Send(new GetLanguagesQuery());
        }

        public async Task<ActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            CreateCategoryCommand createCategoryCommand = new()
            {
                Description = Category.Description,
            };

            short categoryId = await _mediator.Send(createCategoryCommand);


            // Save translations for each language
            foreach (var translation in Translations)
            {
                var createCategoryTranslationCommand = new CreateCategoryTranslationCommand
                {
                    Title = translation.Title,
                    Status = translation.Status,
                    InsertDate = DateTime.UtcNow,
                    PublishDate = translation.PublishDate,
                    LanguageId = translation.LanguageId,
                    CategoryId = categoryId
                };

                ValidationResult result = await _validator.ValidateAsync(createCategoryTranslationCommand);

                if (!result.IsValid)
                {
                    // If any translation fails validation, return to the page and display errors
                    return Page();
                }
            }

            return RedirectToPage("/admin/categories/detail", new { categoryId });
        }
    }
}
