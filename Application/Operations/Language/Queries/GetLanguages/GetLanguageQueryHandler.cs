using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandQueries.Language.Queries.GetLanguages
{
    internal sealed class GetLanguageQueryHandler : IRequestHandler<GetLanguagesQuery, LanguageResponse>
    {
        private readonly IApplicationDbContext _context;

        public GetLanguageQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LanguageResponse> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            var language = await _context
                .Languages
                .Where(p => p.Id == request.Id)
                .Select(p => new LanguageResponse(
                    p.Id,
                    p.Name,
                    p.LanguageCode))
                .FirstOrDefaultAsync(cancellationToken);

            if (language is null)
            {
                throw new LanguageNotFoundException(request.Id);
            }

            return language;
        }
    }
}
