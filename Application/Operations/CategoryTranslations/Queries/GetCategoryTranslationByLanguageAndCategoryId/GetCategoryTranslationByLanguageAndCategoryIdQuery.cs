using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationsByLanguageId;
public record GetCategoryTranslationByLanguageAndCategoryIdQuery : IRequest<CategoryTranslationDto>
{
    public short LanguageId { get; init; }
    public short CategoryId { get; init; }
}

public class GetCategoryTranslationsByLanguageIdQueryHandler : IRequestHandler<GetCategoryTranslationByLanguageAndCategoryIdQuery, CategoryTranslationDto>
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

    public async Task<CategoryTranslationDto> Handle(GetCategoryTranslationByLanguageAndCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var translation = await _context.CategoryTranslations
                    .Where(ct => ct.LanguageId == request.LanguageId &&
                    ct.CategoryId == request.CategoryId)
                    .FirstAsync(cancellationToken);

        if (translation is null)
        {
            throw new LanguageNotFoundException(request.LanguageId);
            throw new CategoryNotFoundException(request.CategoryId);
        }

        var categoryTranslationDto = _mapper.Map<CategoryTranslationDto>(translation);

        return categoryTranslationDto;
    }
}