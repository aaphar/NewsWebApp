using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Operations.Language.Queries.GetLanguageByCode;
using Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageCodeAndNewsId;
using AutoMapper;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationByLanguageCodeAndCategoryI;

public record class GetCategoryTranslationByLanguageCodeAndCategoryIdQuery : IRequest<CategoryTranslationDto>
{
    public string? LanguageCode { get; init; }
    public short CategoryId { get; init; }
}

public class GetCategoryTranslationByLanguageCodeAndCategoryIdQueryHandler 
    : IRequestHandler<GetCategoryTranslationByLanguageCodeAndCategoryIdQuery, CategoryTranslationDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    public GetCategoryTranslationByLanguageCodeAndCategoryIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<CategoryTranslationDto> Handle(GetCategoryTranslationByLanguageCodeAndCategoryIdQuery request, CancellationToken cancellationToken)
    {
        LanguageDto language = await _mediator.Send(new GetLanguageByCodeQuery(request.LanguageCode));

        var translation = await _context.CategoryTranslations            
            .FirstOrDefaultAsync(p => p.LanguageId == language.Id
            && p.CategoryId == request.CategoryId);

        if (translation is null)
        {
            throw new CategoryNotFoundWithGivenLanguageException(request.CategoryId, request.LanguageCode);
        }

        var categoryTranslationDto = _mapper.Map<CategoryTranslationDto>(translation);

        return categoryTranslationDto;
    }
}