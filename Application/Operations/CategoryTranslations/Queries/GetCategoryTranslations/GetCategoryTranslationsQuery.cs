using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.CategoryTranslations.Queries.GetCategoryTranslations;
public record GetCategoryTranslationsQuery : IRequest<List<CategoryTranslationDto>>;

public sealed class GetCategoryTranslationsQueryHandler : IRequestHandler<GetCategoryTranslationsQuery, List<CategoryTranslationDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetCategoryTranslationsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryTranslationDto>> Handle(GetCategoryTranslationsQuery request, CancellationToken cancellationToken)
    {
        var categoryTranslations = await _context.CategoryTranslations
            .ToListAsync(cancellationToken);

        var categoryTranslationDtos = _mapper.Map<List<CategoryTranslationDto>>(categoryTranslations);
        
        return categoryTranslationDtos;
    }
}