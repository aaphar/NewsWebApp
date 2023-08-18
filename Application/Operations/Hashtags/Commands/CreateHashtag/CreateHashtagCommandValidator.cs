using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Hashtags.Commands.CreateHashtag;

public class CreateHashtagCommandValidator:AbstractValidator<CreateHashtagCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateHashtagCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(u => u.Title)
            .NotEmpty()
            .MustAsync(BeUniqueTitle)
            .WithMessage("The specified email address already exists.");
    }

    private async Task<bool> BeUniqueTitle(string? title, CancellationToken cancellationToken)
    {
        return await _context.Hashtags
            .AllAsync(l => l.Title != title, cancellationToken);
    }
}
