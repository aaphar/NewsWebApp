using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationsByLanguageId;
public record GetCategoryTranslationsByLanguageAndCategoryIdQuery : IRequest<CategoryTranslationDto>
{
    public short LanguageId { get; init; }
    public short CategoryId { get; init; }
}

public class GetCategoryTranslationsByLanguageIdQueryHandler : IRequestHandler<GetCategoryTranslationsByLanguageAndCategoryIdQuery, CategoryTranslationDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetCategoryTranslationsByLanguageIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryTranslationDto> Handle(GetCategoryTranslationsByLanguageAndCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var translation = await _context.CategoryTranslations
                    .Where(ct => ct.LanguageId == request.LanguageId &&
                    ct.CategoryId == request.CategoryId)
                    .ToListAsync(cancellationToken);

        return _mapper.Map<CategoryTranslationDto>(translation);
    }
}