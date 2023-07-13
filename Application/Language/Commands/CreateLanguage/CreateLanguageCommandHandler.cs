using MediatR;
using Domain.Repositories;
using Application.Common.Interfaces;

namespace Application.Language.Commands.CreateLanguage
{
    internal sealed class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand>
    {
        private readonly ILanguageRepository _languageRepository;

        private readonly IApplicationDbContext _context;

        public CreateLanguageCommandHandler(
            ILanguageRepository languageRepository,
            IApplicationDbContext context)
        {
            _languageRepository = languageRepository;
            _context = context;
        }

        public async Task Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = new Domain.Entities.Language(
                request.Id,
                request.Name,
                request.LanguageCode);

            _languageRepository.Add(language);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
