using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.CategoryTranslations.Commands.UpdateCategoryTranslation;
public class UpdateCategoryTranslationCommandValidator : AbstractValidator<UpdateCategoryTranslationCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryTranslationCommandValidator(IApplicationDbContext context)
    {
        _context = context;
                
        RuleFor(c => c.Title)
                .NotEmpty()
                .MaximumLength(50)
                .MustAsync(BeUniqueTitle)
                .WithMessage("The specified title already exists.");

        RuleFor(c => c.Status)
            .IsInEnum();

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

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.CategoryTranslations
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}

