using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandQueries.Language.Commands.CreateLanguage
{
    public class CreateLanguageCommandValidator : AbstractValidator<CreateLanguageCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateLanguageCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Code)
                .MaximumLength(5)
                .NotEmpty();

            RuleFor(v => v.Title)
                .MaximumLength(25)
                .NotEmpty()
                .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.Languages
                .AllAsync(l => l.Title != title, cancellationToken);
        }
    }
}
