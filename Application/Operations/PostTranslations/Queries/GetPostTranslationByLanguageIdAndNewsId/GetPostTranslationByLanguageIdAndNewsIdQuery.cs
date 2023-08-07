using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageIdAndNewsId;
public record GetPostTranslationByLanguageIdAndNewsIdQuery:IRequest<PostTranslationDto>
{
    public short LanguageId { get; init; }
    public long NewsId { get; init; }
}

public class GetPostTranslationByLanguageIdAndNewsIdQueryHandler :IRequestHandler<GetPostTranslationByLanguageIdAndNewsIdQuery, PostTranslationDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetPostTranslationByLanguageIdAndNewsIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PostTranslationDto> Handle(GetPostTranslationByLanguageIdAndNewsIdQuery request, CancellationToken cancellationToken)
    {
        var translation = await _context.PostTranslations
            .FirstOrDefaultAsync(p => p.LanguageId == request.LanguageId
            && p.NewsId == request.NewsId);

        if(translation is null)
        {
            throw new LanguageNotFoundException(request.LanguageId);
            throw new PostNotFoundException(request.NewsId);
        }

        var postTranslationDto = _mapper.Map<PostTranslationDto>(translation);

        return postTranslationDto;
    }
}