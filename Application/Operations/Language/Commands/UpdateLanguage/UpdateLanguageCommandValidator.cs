using FluentValidation;

namespace Application.CommandQueries.Language.Commands.CreateLanguage
{
    public class UpdateLanguageCommandValidator:AbstractValidator<CreateLanguageCommand>
    {
        public UpdateLanguageCommandValidator()
        {
            RuleFor(v => v.Code)
                .MaximumLength(5)
                .NotEmpty();

            RuleFor(v => v.Title)
                .MaximumLength(25)
                .NotEmpty();
        }
    }
}
