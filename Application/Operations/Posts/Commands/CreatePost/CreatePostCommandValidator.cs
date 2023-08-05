using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Posts.Commands.CreatePost;
public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    private readonly IApplicationDbContext _context;

    public CreatePostCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(p => p.Title)
                .NotEmpty()
                .MaximumLength(250)
                .MustAsync(BeUniqueTitle)
                .WithMessage("The specified title already exists.");

        RuleFor(p => p.ImagePath)
                .NotEmpty();

        //RuleFor(p => p.PublishDate)
        //    .GreaterThanOrEqualTo(DateTime.Now);

        RuleFor(c => c.CategoryId)
            .NotEmpty()
            .MustAsync(CategoryExists)
            .WithMessage("CategoryId does not exist in the database.");
    }

    private async Task<bool> CategoryExists(short categoryId, CancellationToken cancellationToken)
    {
        return await _context.Categories.AnyAsync(c => c.Id == categoryId, cancellationToken);
    }

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Posts
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}

