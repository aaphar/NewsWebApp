using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.CommandQueries.Language.Commands.DeleteLanguage;
public record DeleteLanguageCommand(short Id) : IRequest<Unit>;

public class DeleteLanguageCommandHandler : IRequestHandler<DeleteLanguageCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteLanguageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = await _context.Languages
                    .Include(l => l.CategoryTranslations)
                    .Include(l => l.PostTranslations)
                    .Where(l => l.Id == request.Id)
                    .SingleOrDefaultAsync(cancellationToken);

        if (language is null)
        {
            throw new LanguageNotFoundException(request.Id);
        }

        // Set the CategoryId to null for related posts
        foreach (var category in language.CategoryTranslations)
        {
            category.LanguageId = null;
        }

        // Set the CategoryId to null for related posts
        foreach (var post in language.PostTranslations)
        {
            post.LanguageId = null;
        }

        _context.Languages.Remove(language);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
