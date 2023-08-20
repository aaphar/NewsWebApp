using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.Operations.Hashtags.Commands.UpdateHashtag;

public record UpdateHashtagCommand : IRequest<Unit>
{
    public long Id { get; init; }
    public string? Title { get; init; }
    public long AuthorId { get; set; }

}

public class UpdateHashtagCommandHandler : IRequestHandler<UpdateHashtagCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateHashtagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateHashtagCommand request, CancellationToken cancellationToken)
    {
        var hashtag = await _context.Hashtags
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (hashtag is null)
        {
            throw new HashtagNotFoundException(request.Id);
        }

        hashtag.Title = request.Title;
        hashtag.LastModified = DateTime.Now;
        hashtag.LastModifiedBy = request.AuthorId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}