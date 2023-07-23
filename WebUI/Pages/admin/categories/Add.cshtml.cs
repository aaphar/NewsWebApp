using Application.CommandQueries.Language.Commands.CreateLanguage;
using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Operations.Categories.Commands.CreateCategory;
using Application.Operations.Categories.Queries.GetCategoryById;
using Application.Operations.CategoryTranslations.Commands.CreateCategoryTranslation;
using Application.Operations.Language.Queries.GetLanguageById;
using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Transactions;

namespace WebUI.Pages.admin.categories
{
    public class AddModel : PageModel
    {
        // category dto
        [BindProperty]
        public CategoryDto? Category { get; set; }

        // category lang
        [BindProperty]
        public string? TranslationTitle { get; set; }

        [BindProperty]
        public Status Status { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime PublishDate { get; set; }

        [BindProperty]
        public string? Code { get; set; } // dilin id yox kodu ile isle, code uygun id tap onu menimsed

        [BindProperty]
        public short LanguageId { get; set; }

        // languages
        [BindProperty]
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
        }

        public async Task OnGetAsync()
        {
            Languages = await _mediator.Send(new GetLanguagesQuery());
        }

        public async Task<ActionResult> OnPostAsync()
        {
            short categoryId = 0;

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                CreateCategoryCommand createCategoryCommand = new()
                {
                    Description = "",
                };

                ValidationResult categoryResult = await _categoryValidator.ValidateAsync(createCategoryCommand);

                if (!categoryResult.IsValid)
                {
                    transactionScope.Dispose();
                }

                categoryId = await _mediator.Send(createCategoryCommand);

                //short languageId = 0;

                //if (Code != null)
                //{
                //    foreach (LanguageDto lang in Languages)
                //    {
                //        if (lang?.LanguageCode?.Trim().ToLower() == Code.Trim().ToLower())
                //        {
                //            languageId = lang.Id;
                //            break; // If you've found a match, no need to continue the loop.
                //        }
                //    }
                //}

                CreateCategoryTranslationCommand createCategoryTranslationCommand = new()
                {
                    Title = TranslationTitle,
                    Status = Status,
                    InsertDate = DateTime.Now,
                    PublishDate = PublishDate,
                    CategoryId = categoryId,
                    LanguageId = LanguageId,
                };

                await _mediator.Send(createCategoryTranslationCommand);

                ValidationResult result = await _validator.ValidateAsync(createCategoryTranslationCommand);

                if (!result.IsValid)
                {
                    transactionScope.Dispose(); // Rollback the transaction if translation creation fails
                }

                transactionScope.Complete(); // Commit the transaction
            }
            return RedirectToPage("/admin/categories/addoredit", new { categoryId });
        }
    }
}
