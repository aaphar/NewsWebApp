using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Categories.Queries.GetCategories;
public record GetCategoriesQuery : IRequest<List<CategoryDto>>;

public sealed class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories
            .ToListAsync(cancellationToken);

        var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

        return categoryDtos;
    }
}

