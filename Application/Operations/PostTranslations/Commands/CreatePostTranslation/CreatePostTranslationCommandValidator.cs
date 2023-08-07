using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.PostTranslations.Commands.CreatePostTranslation;
public class CreatePostTranslationCommandValidator : AbstractValidator<CreatePostTranslationCommand>
{
    private readonly IApplicationDbContext _context;

    public CreatePostTranslationCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(p => p.Title)
                .NotEmpty()
                .MaximumLength(250);
                //.MustAsync(BeUniqueTitle)
                //.WithMessage("The specified title already exists.");

        RuleFor(p => p.Context)
            .NotEmpty();

        //RuleFor(p => p.PublishDate)
        //    .NotEmpty()
        //    .GreaterThanOrEqualTo(DateTime.Now);

        RuleFor(p => p.Status)
                .IsInEnum();

        RuleFor(p => p.LanguageId)
                .NotEmpty()
                .MustAsync(LanguageExists)
                .WithMessage("LanguageId does not exist in the database.");

        RuleFor(p => p.NewsId)
            .NotEmpty()
            .MustAsync(NewsExists)
            .WithMessage("NewsId is not exist in the database");

        RuleFor(p => p.AuthorId)
            .NotEmpty()
            .MustAsync(AuthorExists)
            .WithMessage("AuthorId is not exist in the database");
    }

    private async Task<bool> LanguageExists(short? languageId, CancellationToken cancellationToken)
    {
        return await _context.Languages.AnyAsync(l => l.Id == languageId, cancellationToken);
    }

    private async Task<bool> NewsExists(long newsId, CancellationToken cancellationToken)
    {
        return await _context.Posts.AnyAsync(p => p.Id == newsId, cancellationToken);
    }

    private async Task<bool> AuthorExists(int? authorId, CancellationToken cancellationToken)
    {
        return await _context.MyUsers.AnyAsync(p => p.Id == authorId, cancellationToken);
    }

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.PostTranslations
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}