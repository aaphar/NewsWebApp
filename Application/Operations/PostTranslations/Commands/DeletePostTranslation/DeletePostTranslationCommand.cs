using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.PostTranslations.Commands.DeletePostTranslation;
public record DeletePostTranslationCommand(long Id) : IRequest<Unit>;

public class DeletePostTranslationCommandHandler : IRequestHandler<DeletePostTranslationCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeletePostTranslationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePostTranslationCommand request, CancellationToken cancellationToken)
    {
        var postTranslation = await _context.PostTranslations
            .Where(p => p.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (postTranslation is null)
        {
            throw new PostTranslationNotFoundException(request.Id);
        }
        //deleting hashtag if post will be deleted

        _context.PostTranslations.Remove(postTranslation);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
