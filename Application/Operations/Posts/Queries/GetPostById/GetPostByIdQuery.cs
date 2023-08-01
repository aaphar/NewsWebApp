using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Exceptions;
using MediatR;

namespace Application.Operations.Posts.Queries.GetPostById;
public record GetPostByIdQuery(long Id) : IRequest<PostDto>;

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetPostByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PostDto> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (post is null)
        {
            throw new PostNotFoundException(request.Id);
        }

        var postDto = _mapper.Map<PostDto>(post);

        return postDto;
    }
}