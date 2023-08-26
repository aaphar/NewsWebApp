using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Posts.Queries.GetPostTranslationByTitle;

public record GetPostByTitleQuery(string Title) : IRequest<PostDto>;

public class GetPostByTitleQueryHandler : IRequestHandler<GetPostByTitleQuery, PostDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetPostByTitleQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PostDto> Handle(GetPostByTitleQuery request, CancellationToken cancellationToken)
    {
        string title = request.Title.Replace("-", " ");

        var post = await _context.Posts
            .Include(p => p.PostTranslations)
            .FirstOrDefaultAsync(p => p.Title.ToLower() == title.ToLower());

        return _mapper.Map<PostDto>(post);
    }
}