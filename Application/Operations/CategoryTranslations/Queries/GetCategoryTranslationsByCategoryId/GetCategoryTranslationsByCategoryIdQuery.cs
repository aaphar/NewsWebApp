using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationByCategoryId;
public record GetCategoryTranslationsByCategoryIdQuery : IRequest<List<CategoryTranslationDto>>
{
    public short CategoryId { get; init; }
}


public class GetCategoryTranslationsByCategoryIdQueryHandler : IRequestHandler<GetCategoryTranslationsByCategoryIdQuery, List<CategoryTranslationDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetCategoryTranslationsByCategoryIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryTranslationDto>> Handle(GetCategoryTranslationsByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var categoryTranslations = await _context.CategoryTranslations
                .Where(ct => ct.CategoryId == request.CategoryId)
                .ToListAsync(cancellationToken);

        return _mapper.Map<List<CategoryTranslationDto>>(categoryTranslations);
    }
}