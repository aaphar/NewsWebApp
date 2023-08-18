using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Hashtags.Commands.DeleteHashtag;

public record DeleteHashtagCommand(long Id) : IRequest<Unit>;


public class DeleteHashtagCommandHandler : IRequestHandler<DeleteHashtagCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteHashtagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteHashtagCommand request, CancellationToken cancellationToken)
    {
        var hashtag = await _context.Hashtags
            .Where(h => h.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (hashtag is null)
        {
            throw new HashtagNotFoundException(request.Id);
        }

        var postHashtags = _context.PostHashtags
            .Where(p => p.HashtagId == request.Id).ToList();

        if (postHashtags.Any())
        {
            foreach (var item in postHashtags)
            {
                _context.PostHashtags.Remove(item);
            }
        }

        _context.Hashtags.Remove(hashtag);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
