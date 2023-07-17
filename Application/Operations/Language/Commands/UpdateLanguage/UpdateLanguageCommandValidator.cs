using Application.CommandQueries.Language.Commands.UpdateLanguage;
using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandQueries.Language.Commands.CreateLanguage
{
    public class UpdateLanguageCommandValidator : AbstractValidator<UpdateLanguageCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateLanguageCommandValidator(IApplicationDbContext context)
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
