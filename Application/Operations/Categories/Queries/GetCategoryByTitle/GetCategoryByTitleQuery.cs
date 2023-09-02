using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Categories.Queries.GetCategoryByTitle;

public record GetCategoryByTitleQuery(string Title) : IRequest<CategoryDto>;


public class GetCategoryByTitleQueryHandler 
    : IRequestHandler<GetCategoryByTitleQuery, CategoryDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetCategoryByTitleQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryDto> Handle(GetCategoryByTitleQuery request, CancellationToken cancellationToken)
    {
        string title = request.Title.Replace("-", " ");

        var categoryTranslation = await _context.CategoryTranslations
            .FirstOrDefaultAsync(x => x.Title.ToLower() == title.ToLower());    

        if (categoryTranslation == null)
        {
            return null;
        }

        var category= await _context.Categories
            .Include(c=>c.Posts)
            .FirstOrDefaultAsync(p => p.Id == categoryTranslation.CategoryId);

        return _mapper.Map<CategoryDto>(category);
    }
}