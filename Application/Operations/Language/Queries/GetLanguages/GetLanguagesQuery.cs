using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandQueries.Language.Queries.GetLanguages;
public record GetLanguagesQuery : IRequest<List<LanguageDto>>;

public sealed class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, List<LanguageDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetLanguagesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<LanguageDto>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
    {
        var languages = await _context.Languages
            .ToListAsync(cancellationToken);

        var languagesDto = _mapper.Map<List<LanguageDto>>(languages);
        return languagesDto;
    }
}