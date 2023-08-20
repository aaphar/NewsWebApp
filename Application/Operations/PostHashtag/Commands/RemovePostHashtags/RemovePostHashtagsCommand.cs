using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.PostHashtag.Commands.RemovePostHashtags;

public record RemovePostHashtagsCommand : IRequest<Unit>
{
    public long PostTranslationId { get; set; }
}

public class RemovePostHashtagsCommandHandler : IRequestHandler<RemovePostHashtagsCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public RemovePostHashtagsCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(RemovePostHashtagsCommand request, CancellationToken cancellationToken)
    {
        var postHashtagsToRemove = await _context.PostHashtags
            .Where(ph => ph.NewsId == request.PostTranslationId)
            .ToListAsync(cancellationToken);

        _context.PostHashtags.RemoveRange(postHashtagsToRemove);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}