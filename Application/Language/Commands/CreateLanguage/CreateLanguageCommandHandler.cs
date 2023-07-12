using MediatR;
using Domain.Entities;
using Application.Common.Interfaces;

namespace Application.Language.Commands.CreateLanguage
{
    public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateLanguageCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = new Domain.Entities.Language
            {
                Name = request.Name,
                LanguageCode = request.LanguageCode
            };

            _context.Languages.Add(language);

            await _context.SaveChangesAsync(cancellationToken);

            return language.Id;
        }
    }
}
