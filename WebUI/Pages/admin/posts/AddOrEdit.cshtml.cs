using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationByCategoryId;
using Application.Operations.Posts.Queries.GetPostById;
using Application.Operations.PostTranslations.Commands.CreatePostTranslation;
using Application.Operations.PostTranslations.Commands.UpdatePostTranslation;
using Application.Operations.PostTranslations.Queries.GetPostTranslationsByNewsId;
using Domain.Entities;
using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.posts
{
    [Authorize(Roles = "Adminstrator")]
    public class AddOrEditModel : PageModel
    {
        [BindProperty]
        public string? Title { get; set; }

        [BindProperty]
        public string? Content { get; set; }

        [BindProperty]
        public int AuthorId { get; set; }

        [BindProperty]
        public List<PostTranslationDto>? Translations { get; set; }

        [BindProperty]
        public PostTranslationDto? TranslationDto { get; set; }

        [BindProperty]
        public PostDto? PostDto { get; set; }

        [BindProperty]
        public List<LanguageDto>? Languages { get; set; }

        [BindProperty]
        public List<CategoryDto>? Categories { get; set; }

        private readonly IMediator _mediator;

        private readonly IValidator<UpdatePostTranslationCommand> _validator;
        private readonly IValidator<CreatePostTranslationCommand> _createValidator;
        private readonly UserManager<User> _userManager; // Add UserManager

        public AddOrEditModel(
            IMediator mediator,
             IValidator<UpdatePostTranslationCommand> validator,
             IValidator<CreatePostTranslationCommand> createValidator,
             UserManager<User> userManager) // Inject UserManager
        {
            _mediator = mediator;
            _validator = validator;
            _createValidator = createValidator;
            _userManager = userManager;
        }

        public async Task OnGetAsync(long id)
        {
            PostDto = await _mediator.Send(new GetPostByIdQuery(id));

            Languages = await _mediator.Send(new GetLanguagesQuery());

            Categories = await _mediator.Send(new GetCategoriesQuery());

            Translations = await _mediator.Send(new GetPostTranslationsByNewsIdQuery() { NewsId = id });

            var loggedInUserId = _userManager.GetUserId(User);
            AuthorId = int.Parse(loggedInUserId); // Convert to int if needed
        }

        public async Task<ActionResult> OnPostAsync(long id)
        {
            var loggedInUserId = _userManager.GetUserId(User);
            AuthorId = int.Parse(loggedInUserId); // Convert to int if needed

            UpdatePostTranslationCommand updatePostTranslationCommand = new()
            {
                Title = Title,
                Context = Content,
                PublishDate = TranslationDto?.PublishDate ?? DateTime.Now,
                Status = TranslationDto?.Status ?? Status.Draft,
                NewsId = id,
                LanguageId = TranslationDto?.LanguageId ?? null,
                ViewCount = TranslationDto.ViewCount,
                AuthorId = AuthorId
            };

            ValidationResult result = await _validator.ValidateAsync(updatePostTranslationCommand);

            await _mediator.Send(updatePostTranslationCommand);

            return RedirectToPage("/admin/posts/AddOrEdit", new { Id = id });
        }
    }
}
