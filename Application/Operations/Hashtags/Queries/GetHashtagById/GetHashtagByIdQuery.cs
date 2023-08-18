using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Exceptions;
using MediatR;

namespace Application.Operations.Hashtags.Queries.GetHashtagById;

public record GetHashtagByIdQuery(long Id) : IRequest<HashtagDto>;


public class GetHashtagByIdQueryHandler : IRequestHandler<GetHashtagByIdQuery, HashtagDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetHashtagByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HashtagDto> Handle(GetHashtagByIdQuery request, CancellationToken cancellationToken)
    {
        var hashtag = await _context.Hashtags
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (hashtag is null)
        {
            throw new HashtagNotFoundException(request.Id);
        }

        var hashtagDto = _mapper.Map<HashtagDto>(hashtag);

        return hashtagDto;
    }
}