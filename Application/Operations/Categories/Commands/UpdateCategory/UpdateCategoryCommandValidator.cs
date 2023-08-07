using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.Description)
               .MaximumLength(50)
               .MustAsync(BeUniqueTitle)
               .WithMessage("The specified title already exists.");
        }

        private async Task<bool> BeUniqueTitle(string? title, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AllAsync(l => l.Description != title, cancellationToken);
        }
    }
}
