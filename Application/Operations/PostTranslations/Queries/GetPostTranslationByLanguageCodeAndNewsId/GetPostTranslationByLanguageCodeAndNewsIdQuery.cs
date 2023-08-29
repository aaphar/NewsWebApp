using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Operations.Language.Queries.GetLanguageByCode;
using Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageIdAndNewsId;
using AutoMapper;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageCodeAndNewsId;
public record GetPostTranslationByLanguageCodeAndNewsIdQuery : IRequest<PostTranslationDto>
{
    public string? LanguageCode { get; init; }
    public long NewsId { get; init; }
}

public class GetPostTranslationByLanguageCodeAndNewsIdQueryHandler : IRequestHandler<GetPostTranslationByLanguageCodeAndNewsIdQuery, PostTranslationDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    public GetPostTranslationByLanguageCodeAndNewsIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper,
        IMediator mediator)
    {
        _context = context;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<PostTranslationDto> Handle(GetPostTranslationByLanguageCodeAndNewsIdQuery request, CancellationToken cancellationToken)
    {
        LanguageDto language = await _mediator.Send(new GetLanguageByCodeQuery(request.LanguageCode));

        var translation = await _context.PostTranslations
            .Include(p => p.PostHashtags)
            .FirstOrDefaultAsync(p => p.LanguageId == language.Id
            && p.NewsId == request.NewsId);

        if (translation is null)
        {
            throw new PostNotFoundWithGivenLanguageException(request.NewsId, request.LanguageCode);
        }

        var postTranslationDto = _mapper.Map<PostTranslationDto>(translation);

        return postTranslationDto;
    }
}