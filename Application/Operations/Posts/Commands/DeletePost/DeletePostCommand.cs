using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Posts.Commands.DeletePost;
public record DeletePostCommand(long Id) : IRequest<Unit>;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeletePostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
            .Where(p => p.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (post is null)
        {
            throw new PostNotFoundException(request.Id);
        }

        var translations = _context.PostTranslations
            .Where(p => p.NewsId == request.Id).ToList();

        if (translations.Any())
        {
            foreach (var translation in translations)
            {
                _context.PostTranslations.Remove(translation);
            }
        }

        _context.Posts.Remove(post);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}