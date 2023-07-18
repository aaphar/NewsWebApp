using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Operations.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(c => c.Description)
                .MaximumLength(100)
                .NotEmpty();
        }
    }
}
