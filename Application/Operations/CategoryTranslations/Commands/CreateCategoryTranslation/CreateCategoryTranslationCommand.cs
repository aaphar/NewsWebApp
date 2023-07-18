using Application.Common.Interfaces;
using Domain.Enums;
using Domain.Entities;
using MediatR;
using Domain.Events;

namespace Application.Operations.CategoryTranslations.Commands.CreateCategoryTranslation;
public record CreateCategoryTranslationCommand : IRequest<int>
{
    public string? Title { get; init; }

    public Status Status { get; init; }

    public DateTime InsertDate { get; init; }

    public DateTime PublishDate { get; init; }

    public short LanguageId { get; init; }

    public short CategoryId { get; init; }
}

public class CreateCategoryTranslationCommandHandler : IRequestHandler<CreateCategoryTranslationCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryTranslationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCategoryTranslationCommand request, CancellationToken cancellationToken)
    {
        var categoryTranslation = new CategoryTranslation
        {
            Title = request.Title,
            Status = request.Status,
            InsertDate = request.InsertDate,
            PublishDate = request.PublishDate,
            LanguageId = request.LanguageId,
            CategoryId = request.CategoryId,
        };

        _context.CategoryTranslations.Add(categoryTranslation);

        categoryTranslation.AddDomainEvent(new CategoryTranslationCreatedEvent(categoryTranslation));

        await _context.SaveChangesAsync(cancellationToken);

        return categoryTranslation.Id;
    }
}