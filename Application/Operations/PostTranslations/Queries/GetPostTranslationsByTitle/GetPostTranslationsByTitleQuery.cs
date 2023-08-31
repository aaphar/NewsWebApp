using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.PostTranslations.Queries.GetPostSearchsByTitle;

public record GetPostTranslationsByTitleQuery(string searchTerm)
    : IRequest<List<PostTranslationDto>>;


public class GetPostSearchsByTitleQueryHandler
    : IRequestHandler<GetPostTranslationsByTitleQuery, List<PostTranslationDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetPostSearchsByTitleQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PostTranslationDto>> Handle(GetPostTranslationsByTitleQuery request, CancellationToken cancellationToken)
    {
        var postTranslations = await _context.PostTranslations
            .Where(p => p.Title.ToLower().Contains(request.searchTerm.ToLower()))
            .ToListAsync(cancellationToken);
        

        return _mapper.Map<List<PostTranslationDto>>(postTranslations);
    }
}
