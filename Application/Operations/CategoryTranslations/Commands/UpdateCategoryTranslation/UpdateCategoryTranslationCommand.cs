using Application.Common.Interfaces;
using Application.Operations.CategoryTranslations.Commands.CreateCategoryTranslation;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.CategoryTranslations.Commands.UpdateCategoryTranslation;
public record UpdateCategoryTranslationCommand : IRequest<Unit>
{
    public string? Title { get; init; }

    public Status Status { get; init; }

    public DateTime PublishDate { get; init; }

    public short LanguageId { get; init; }

    public short CategoryId { get; init; }
}

public class UpdateCategoryTranslationCommandHandler : IRequestHandler<UpdateCategoryTranslationCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    private readonly IMediator _mediator;

    public UpdateCategoryTranslationCommandHandler(
        IApplicationDbContext context,
        IMediator mediator)
    {
        _context = context;
        _mediator = mediator;

    }

    public async Task<Unit> Handle(UpdateCategoryTranslationCommand request, CancellationToken cancellationToken)
    {
        var categoryTranslation = await _context.CategoryTranslations
            .FirstOrDefaultAsync(
            ct => ct.LanguageId == request.LanguageId &&
            ct.CategoryId == request.CategoryId,
            cancellationToken);
        //.FindAsync(new object[] { request.Id }, cancellationToken);

        if (categoryTranslation is null)
        {
            CreateCategoryTranslationCommand createCategoryTranslationCommand = new()
            {
                Title = request.Title,
                Status = request.Status,
                PublishDate = request.PublishDate,
                CategoryId = request.CategoryId,
                LanguageId = request.LanguageId,
            };

            int id = await _mediator.Send(createCategoryTranslationCommand);
        }
        else
        {
            categoryTranslation.Title = request.Title;
            categoryTranslation.Status = request.Status;
            categoryTranslation.PublishDate = request.PublishDate;
            categoryTranslation.LanguageId = request.LanguageId;
            categoryTranslation.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);
        }
        return Unit.Value;
    }
}