using Application.Common.Interfaces;
using Domain.Exceptions;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandQueries.Language.Commands.UpdateLanguage
{
    internal sealed class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand>
    {
        private readonly ILanguageRepository _languageRepository;

        private readonly IApplicationDbContext _context;

        public UpdateLanguageCommandHandler(
            ILanguageRepository languageRepository,
            IApplicationDbContext context)
        {
            _languageRepository = languageRepository;
            _context = context;
        }
        public async Task Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _languageRepository.GetByIdAsync(request.Id);

            if (language is null)
            {
                throw new LanguageNotFoundException(request.Id);
            }

            language.Update(request.Name, request.LanguageCode);

            _languageRepository.Update(language);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
