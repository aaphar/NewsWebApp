using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.CategoryTranslations.Commands.CreateCategoryTranslation
{
    public class CreateCategoryTranslationCommandValidator : AbstractValidator<CreateCategoryTranslationCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateCategoryTranslationCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.Title)
                .NotEmpty()
                .MaximumLength(50); // Adjust the maximum length as needed

            RuleFor(c => c.Status)
                .IsInEnum();

            RuleFor(c => c.InsertDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.UtcNow); // Assuming you don't want past dates

            RuleFor(c => c.PublishDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.UtcNow); // Assuming you don't want past dates

            RuleFor(c => c.LanguageId)
                .NotEmpty()
                .MustAsync(LanguageExists)
                .WithMessage("LanguageId does not exist in the database.");

            RuleFor(c => c.CategoryId)
                .NotEmpty()
                .MustAsync(CategoryExists)
                .WithMessage("CategoryId does not exist in the database.");
        }

        private async Task<bool> LanguageExists(short languageId, CancellationToken cancellationToken)
        {
            // Implement the method to check if the LanguageId exists in the database
            return await _context.Languages.AnyAsync(l => l.Id == languageId, cancellationToken);
        }

        private async Task<bool> CategoryExists(short categoryId, CancellationToken cancellationToken)
        {
            // Implement the method to check if the CategoryId exists in the database
            return await _context.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
        }
    }
}
