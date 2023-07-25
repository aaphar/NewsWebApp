using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Language.Queries.GetLanguageByCode;
public record GetLanguageByCodeQuery(string Code) : IRequest<LanguageDto>;

public class GetLanguageByCodeQueryHandler : IRequestHandler<GetLanguageByCodeQuery, LanguageDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLanguageByCodeQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LanguageDto> Handle(GetLanguageByCodeQuery request, CancellationToken cancellationToken)
    {
        var language = await _context.Languages
            .FirstOrDefaultAsync(l => l.LanguageCode == request.Code);

        if (language == null)
        {
            throw new LanguageCodeNotFoundException(request.Code);
        }

        var languageDto = _mapper.Map<LanguageDto>(language);

        return languageDto;
    }
}
