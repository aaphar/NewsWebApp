using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Roles.Commands.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateRoleCommandValidator(IApplicationDbContext context)
        {
            _context = context;

           RuleFor(r=>r.Title)
                .MaximumLength(25)
                .NotEmpty()
                .MustAsync(BeUniqueTitle)
                .WithMessage("The specified title already exists.");
        }

        private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.Languages
                .AllAsync(l => l.Title != title, cancellationToken);
        }
    }
}
