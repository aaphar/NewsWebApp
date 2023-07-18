using Application.Common.Interfaces;
using Domain.Enums;
using Domain.Exceptions;
using MediatR;

namespace Application.Operations.CategoryTranslations.Commands.UpdateCategoryTranslation;
public record UpdateCategoryTranslationCommand : IRequest<Unit>
{
    public int Id { get; init; }

    public string? Title { get; init; }

    public Status Status { get; init; }

    public DateTime InsertDate { get; init; }

    public DateTime PublishDate { get; init; }

    public short LanguageId { get; init; }

    public short CategoryId { get; init; }
}

public class UpdateCategoryTranslationCommandHandler : IRequestHandler<UpdateCategoryTranslationCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryTranslationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateCategoryTranslationCommand request, CancellationToken cancellationToken)
    {
        var categoryTranslation = await _context.CategoryTranslations
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (categoryTranslation is null)
        {
            throw new CategoryTranslationNotFoundException(request.Id);
        }

        categoryTranslation.Title = request.Title;
        categoryTranslation.Status = request.Status;
        categoryTranslation.InsertDate = request.InsertDate;
        categoryTranslation.PublishDate = request.PublishDate;
        categoryTranslation.LanguageId = request.LanguageId;
        categoryTranslation.CategoryId = request.CategoryId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}