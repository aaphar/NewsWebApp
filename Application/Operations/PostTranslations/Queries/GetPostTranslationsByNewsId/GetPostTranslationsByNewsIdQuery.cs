using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.PostTranslations.Queries.GetPostTranslationsByNewsId;
public record GetPostTranslationsByNewsIdQuery : IRequest<List<PostTranslationDto>>
{
    public long NewsId { get; init; }
}


public class GetPostTranslationsByNewsIdQueryHandler : IRequestHandler<GetPostTranslationsByNewsIdQuery, List<PostTranslationDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetPostTranslationsByNewsIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PostTranslationDto>> Handle(GetPostTranslationsByNewsIdQuery request, CancellationToken cancellationToken)
    {
        var translations = await _context.PostTranslations
            .Where(pt => pt.NewsId == request.NewsId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<PostTranslationDto>>(translations);
    }
}