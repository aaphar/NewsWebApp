using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Categories.Commands.DeleteCategory;
public record DeleteCategoryCommand(short Id) : IRequest<Unit>;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .Include(c => c.Posts)  // Include the Posts collection
            .Where(c => c.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (category is null)
        {
            throw new CategoryNotFoundException(request.Id);
        }

        // Set the CategoryId to null for related posts
        foreach (var post in category.Posts)
        {
            post.CategoryId = null;
        }

        var translations = _context.CategoryTranslations
            .Where(c => c.CategoryId == request.Id).ToList();

        if (translations.Any())
        {
            foreach (var translation in translations)
            {
                _context.CategoryTranslations.Remove(translation);
            }
        }

        _context.Categories.Remove(category);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}