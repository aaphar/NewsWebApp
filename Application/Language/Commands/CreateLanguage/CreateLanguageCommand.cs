using MediatR;

namespace Application.Language.Commands.CreateLanguage;
public record CreateLanguageCommand(
    short Id,
    string Name,
    string LanguageCode) : IRequest;

