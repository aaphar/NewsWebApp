using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Posts.Commands.UpdatePost;
public class UpdatePostCommandValidator:AbstractValidator<UpdatePostCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdatePostCommandValidator(IApplicationDbContext context)
    {
        _context = context;

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
}
