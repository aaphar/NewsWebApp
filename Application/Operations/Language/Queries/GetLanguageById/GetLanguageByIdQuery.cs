using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Exceptions;
using MediatR;
using System.Linq;

namespace Application.Operations.Language.Queries.GetLanguageById;
public record GetLanguageByIdQuery : IRequest<LanguageDto>
{
    public short Id { get; init; }
}

public class GetLanguageByIdQueryHandler : IRequestHandler<GetLanguageByIdQuery, LanguageDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetLanguageByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LanguageDto> Handle(GetLanguageByIdQuery request, CancellationToken cancellationToken)
    {
        var language = await _context.Languages.FindAsync(new object[] { request.Id }, cancellationToken);


        if (language is null)
        {
            throw new LanguageNotFoundException(request.Id);
        }

        var languageDto = _mapper.Map<LanguageDto>(language);

        return languageDto;
    }
}

