using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.Language.Queries.GetLanguageByCode;
using Application.Operations.Posts.Commands.CreatePost;
using Application.Operations.PostTranslations.Commands.CreatePostTranslation;
using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Transactions;

namespace WebUI.Pages.admin.posts
{
    [IgnoreAntiforgeryToken]
    public class AddModel : PageModel
    {
        [BindProperty]
        public string? Title { get; set; }

        [BindProperty]
        public string? Content { get; set; }

        [BindProperty]
        public string? ImagePath { get; set; }

        [BindProperty]
        public Status Status { get; set; }

        [BindProperty]
        public DateTime PublishDate { get; set; }

        public long ViewCount { get; set; }

        public short LanguageId { get; set; }

        public int AuthorId { get; set; }

        [BindProperty]
        public short? CategoryId { get; set; }

        public List<CategoryDto>? Categories { get; set; }

        public LanguageDto? DefaultLanguage { get; set; }

        // languages
        [BindProperty]
        public List<LanguageDto>? Languages { get; set; }


        private readonly IMediator _mediator;

        private readonly IValidator<CreatePostCommand> _postValidator;

        private readonly IValidator<CreatePostTranslationCommand> _validator;

        private readonly IWebHostEnvironment _environment;

        private readonly ILogger<AddModel> _logger;

        public AddModel(
            IMediator mediator,
            IValidator<CreatePostCommand> postValidator,
            IValidator<CreatePostTranslationCommand> validator,
            IWebHostEnvironment environment,
            ILogger<AddModel> logger)
        {
            _mediator = mediator;
            _postValidator = postValidator;
            _validator = validator;
            _environment = environment;
            _logger = logger;
        }

        public async Task OnGet()
        {
            PublishDate = DateTime.Today;

            Categories = await _mediator.Send(new GetCategoriesQuery());

            Languages = await _mediator.Send(new GetLanguagesQuery());

            DefaultLanguage = await _mediator.Send(new GetLanguageByCodeQuery(DefaultLanguageCode.Code));

            LanguageId = DefaultLanguage.Id;
        }


        public async Task<ActionResult> OnPostAsync()
        {
            long postId = 0;

            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Retrieve the stored image URL from TempData
                var uploadedImagePath = TempData["UploadedImagePath"] as string;
                if (!string.IsNullOrEmpty(uploadedImagePath))
                {
                    // Update the ImagePath property with the uploaded image URL
                    ImagePath = uploadedImagePath;
                }

                CreatePostCommand createPostCommand = new()
                {
                    Title = Title,
                    ImagePath = ImagePath ?? null,
                    PublishDate = PublishDate,
                    CategoryId = CategoryId ?? null
                };

                ValidationResult postResult = await _postValidator.ValidateAsync(createPostCommand);

                if (!postResult.IsValid)
                {
                    scope.Dispose();

                    return Page();
                }

                postId = await _mediator.Send(createPostCommand);

                DefaultLanguage = await _mediator.Send(new GetLanguageByCodeQuery(DefaultLanguageCode.Code));

                LanguageId = DefaultLanguage.Id;

                // title validation has problem
                CreatePostTranslationCommand createPostTranslation = new()
                {
                    Title = Title?.Substring(0, 1).ToUpper() + Title?.Substring(1).ToLower(),
                    Context = Content,
                    PublishDate = PublishDate,
                    Status = Status,
                    NewsId = postId,
                    LanguageId = LanguageId,
                    ViewCount = ViewCount,
                    AuthorId = 1
                };

                await _mediator.Send(createPostTranslation);

                ValidationResult result = await _validator.ValidateAsync(createPostTranslation);

                if (!result.IsValid)
                {
                    scope.Dispose();
                    return Page();
                }

                TempData["UploadedImagePath"] = null;

                scope.Complete();
            };

            return RedirectToPage("/admin/posts/detail", new { Id = postId });
        }
    }
}