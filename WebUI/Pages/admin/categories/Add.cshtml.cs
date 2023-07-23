using Application.CommandQueries.Language.Commands.CreateLanguage;
using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Operations.Categories.Commands.CreateCategory;
using Application.Operations.CategoryTranslations.Commands.CreateCategoryTranslation;
using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Transactions;

namespace WebUI.Pages.admin.categories
{
    public class AddModel : PageModel
    {
        // her bir dili goturmeliyem list landtos
        // langs list foreach her biri ucun post

        // category dto
        [BindProperty]
        public CategoryDto? Category { get; set; }

        // category lang
        public string? Title { get; set; }

        public Status Status { get; set; }

        public DateTime InsertDate { get; set; }

        public DateTime PublishDate { get; set; }

        public string? Code { get; set; } // dilin id yox kodu ile isle, code uygun id tap onu menimsed

        public short CategoryId { get; set; }

        // languages
        [BindProperty]
        public List<LanguageDto>? Languages { get; set; }

        private readonly IApplicationDbContext _context;

        private readonly IMediator _mediator;

        private readonly IValidator<CreateCategoryCommand> _categoryValidator;

        private readonly IValidator<CreateCategoryTranslationCommand> _validator;

        public AddModel(
            IApplicationDbContext context,
            IMediator mediator,
            IValidator<CreateCategoryCommand> categoryValidator,
            IValidator<CreateCategoryTranslationCommand> validator)
        {
            _context = context;
            _mediator = mediator;
            _categoryValidator = categoryValidator;
            _validator = validator;
        }

        public async Task OnGetAsync()
        {
            Languages = await _mediator.Send(new GetLanguagesQuery());
        }

        public async Task OnPostAsync()
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    CreateCategoryCommand createCategoryCommand = new()
                    {
                        Description = "",
                    };

                    short categoryId = await _mediator.Send(createCategoryCommand);

                    // Find the default language based on the language code (assuming default language code is stored in 'Code' variable)
                    LanguageDto? defaultLanguage = Languages?.FirstOrDefault(lang => lang.LanguageCode == "az");

                    foreach (LanguageDto lang in Languages)
                    {
                        short id = 0;

                        if (lang.LanguageCode == Code)
                        {
                            id = lang.Id;
                        }

                        CreateCategoryTranslationCommand createCategoryTranslationCommand = new()
                        {
                            Title = Title,
                            Status = Status,
                            InsertDate = DateTime.Now,
                            PublishDate = PublishDate,
                            LanguageId = id,
                            CategoryId = categoryId
                        };

                        ValidationResult result = await _validator.ValidateAsync(createCategoryTranslationCommand);
                        await _mediator.Send(createCategoryTranslationCommand);

                        if (!result.IsValid)
                        {
                            transactionScope.Dispose(); // Rollback the transaction if translation creation fails
                        }

                        if (lang.Id == defaultLanguage?.Id)
                        {
                            createCategoryCommand.Description = Title;
                            ValidationResult cResult = await _categoryValidator.ValidateAsync(createCategoryCommand);
                            await _mediator.Send(createCategoryCommand);
                        }
                    }
                }
                catch (Exception)
                {
                    transactionScope.Dispose();
                    throw;
                }

                transactionScope.Complete(); // Commit the transaction
            }
        }
    }
}
