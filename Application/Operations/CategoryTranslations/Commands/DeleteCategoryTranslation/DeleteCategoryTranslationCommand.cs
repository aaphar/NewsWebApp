using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.CategoryTranslations.Commands.DeleteCategoryTranslation;
public record DeleteCategoryTranslationCommand(int Id) : IRequest<Unit>;

public class DeleteCategoryTranslationCommandHandler : IRequestHandler<DeleteCategoryTranslationCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteCategoryTranslationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCategoryTranslationCommand request, CancellationToken cancellationToken)
    {
        var categoryTranslation = await _context.CategoryTranslations
                    .Where(c => c.Id == request.Id)
                    .SingleOrDefaultAsync(cancellationToken);

        if (categoryTranslation is null)
        {
            throw new CategoryTranslationNotFoundException(request.Id);
        }

        _context.CategoryTranslations.Remove(categoryTranslation);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
