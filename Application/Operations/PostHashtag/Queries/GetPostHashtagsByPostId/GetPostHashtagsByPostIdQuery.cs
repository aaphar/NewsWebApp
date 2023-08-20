using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.PostHashtag.Queries.GetPostHashtagsByPostId;

public record GetPostHashtagsByPostIdQuery(long PostId) : IRequest<List<PostHashtagDto>>;


public class GetPostHashtagsByPostIdQueryHandler : IRequestHandler<GetPostHashtagsByPostIdQuery, List<PostHashtagDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetPostHashtagsByPostIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PostHashtagDto>> Handle(GetPostHashtagsByPostIdQuery request, CancellationToken cancellationToken)
    {
        var postHashtags = await _context.PostHashtags
            .Where(h => h.NewsId == request.PostId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<PostHashtagDto>>(postHashtags);
    }
}