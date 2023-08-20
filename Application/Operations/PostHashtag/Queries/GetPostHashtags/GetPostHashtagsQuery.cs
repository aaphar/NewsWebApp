using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.PostHashtag.Queries.GetPostHashtags;

public record GetPostHashtagsQuery : IRequest<List<PostHashtagDto>>;


public class GetPostHashtagsQueryHandler : IRequestHandler<GetPostHashtagsQuery, List<PostHashtagDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetPostHashtagsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PostHashtagDto>> Handle(GetPostHashtagsQuery request, CancellationToken cancellationToken)
    {
        var posthashtags = await _context.PostHashtags
            .ToListAsync(cancellationToken);

        var hashtagDtos = _mapper.Map<List<PostHashtagDto>>(posthashtags);

        return hashtagDtos;
    }
}