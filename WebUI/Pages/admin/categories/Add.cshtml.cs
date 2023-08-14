using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Categories.Commands.CreateCategory;
using Application.Operations.CategoryTranslations.Commands.CreateCategoryTranslation;
using Application.Operations.Language.Queries.GetLanguageByCode;
using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Transactions;

namespace WebUI.Pages.admin.categories
{
    [Authorize(Roles = "Adminstrator")]
    public class AddModel : PageModel
    {
        // category lang
        [BindProperty]
        public string? TranslationTitle { get; set; }

        [BindProperty]
        public Status Status { get; set; }

        [BindProperty]
        public DateTime PublishDate { get; set; }
           

        public short LanguageId { get; set; }

        public LanguageDto? DefaultLanguage { get; set; }

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
            PublishDate = DateTime.Today;

            Languages = await _mediator.Send(new GetLanguagesQuery());

            DefaultLanguage = await _mediator.Send(new GetLanguageByCodeQuery(DefaultLanguageCode.Code));

            LanguageId = DefaultLanguage.Id;
        }

        public async Task<ActionResult> OnPostAsync()
        {
            short categoryId = 0;

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                CreateCategoryCommand createCategoryCommand = new()
                {
                    Description = TranslationTitle
                };

                ValidationResult categoryResult = await _categoryValidator.ValidateAsync(createCategoryCommand);

                if (!categoryResult.IsValid)
                {
                    transactionScope.Dispose();

                    return Page();
                }

                categoryId = await _mediator.Send(createCategoryCommand);

                DefaultLanguage = await _mediator.Send(new GetLanguageByCodeQuery(DefaultLanguageCode.Code));

                LanguageId = DefaultLanguage.Id;

                CreateCategoryTranslationCommand createCategoryTranslationCommand = new()
                {
                    Title = TranslationTitle?.Substring(0, 1).ToUpper() + TranslationTitle?.Substring(1).ToLower(),

                    Status = Status,

                    PublishDate = PublishDate,

                    CategoryId = categoryId,
                    
                    LanguageId = LanguageId,
                };

                await _mediator.Send(createCategoryTranslationCommand);

                ValidationResult result = await _validator.ValidateAsync(createCategoryTranslationCommand);

                if (!result.IsValid)
                {
                    transactionScope.Dispose(); 
                    return Page();
                }

                transactionScope.Complete(); 
            }
            return RedirectToPage("/admin/categories/index");
        }
    }
}
