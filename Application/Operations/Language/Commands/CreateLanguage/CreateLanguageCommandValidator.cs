using FluentValidation;

namespace Application.CommandQueries.Language.Commands.CreateLanguage
{
    public class CreateLanguageCommandValidator:AbstractValidator<CreateLanguageCommand>
    {
        public CreateLanguageCommandValidator()
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
