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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.categories
{
    [Authorize(Roles = "Adminstrator")]
    public class AddOrEditModel : PageModel
    {
        [BindProperty]
        public List<CategoryTranslationDto>? Translations { get; set; }

        [BindProperty]
        public CategoryTranslationDto? TranslationDto { get; set; }

        [BindProperty]
        public List<LanguageDto>? Languages { get; set; }

        public long AuthorId { get; set; }

        private readonly UserManager<User> _userManager; // Add UserManager

        private readonly IMediator _mediator;

        private readonly IValidator<UpdateCategoryTranslationCommand> _validator;
        private readonly IValidator<CreateCategoryTranslationCommand> _createValidator;

        public AddOrEditModel(
            IMediator mediator,
             IValidator<UpdateCategoryTranslationCommand> validator,
             IValidator<CreateCategoryTranslationCommand> createValidator,
             UserManager<User> userManager)
        {
            _mediator = mediator;
            _validator = validator;
            _createValidator = createValidator;
            _userManager = userManager;
        }

        public async Task OnGetAsync(short id)
        {
            Languages = await _mediator.Send(new GetLanguagesQuery());

            Translations = await _mediator.Send(new GetCategoryTranslationsByCategoryIdQuery() { CategoryId = id });

            // Get the currently logged-in user's Id
            var loggedInUserId = _userManager.GetUserId(User);
            AuthorId = int.Parse(loggedInUserId); // Convert to int if needed
        }

        public async Task<ActionResult> OnPostAsync(short id)
        {
            UpdateCategoryTranslationCommand updateTranslation = new()
            {
                Title = TranslationDto?.Title?.Substring(0, 1).ToUpper() + TranslationDto?.Title?.Substring(1).ToLower(),
                //Status = TranslationDto.Status,
                Status = TranslationDto?.Status ?? Status.Draft,
                PublishDate = TranslationDto?.PublishDate ?? DateTime.Now,
                CategoryId = id,
                LanguageId = TranslationDto?.LanguageId ?? null,
                AuthorId = AuthorId
            };

            ValidationResult result = await _validator.ValidateAsync(updateTranslation);

            await _mediator.Send(updateTranslation);

            return RedirectToPage("/admin/categories/AddOrEdit", new { Id = id });

        }
    }
}
