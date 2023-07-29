using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Exceptions;
using MediatR;

namespace Application.Operations.CategoryTranslations.Queries.GetCategoryTranslationById;
public record GetCategoryTranslationByIdQuery : IRequest<CategoryTranslationDto>
{
    public int Id { get; init; }
}

public class GetCategoryTranslationByIdQueryHandler : IRequestHandler<GetCategoryTranslationByIdQuery, CategoryTranslationDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetCategoryTranslationByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryTranslationDto> Handle(GetCategoryTranslationByIdQuery request, CancellationToken cancellationToken)
    {
        var categoryTranslation = await _context.CategoryTranslations
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (categoryTranslation is null)
        {
            throw new CategoryTranslationNotFoundException(request.Id);
        }

        var categoryTranslationDto = _mapper.Map<CategoryTranslationDto>(categoryTranslation);

        return categoryTranslationDto;
    }
}