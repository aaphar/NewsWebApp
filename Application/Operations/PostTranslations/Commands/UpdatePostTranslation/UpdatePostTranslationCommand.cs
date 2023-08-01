using Application.CommandQueries.Language.Commands.CreateLanguage;
using Application.Common.Interfaces;
using Application.Operations.PostTranslations.Commands.CreatePostTranslation;
using Domain.Enums;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.PostTranslations.Commands.UpdatePostTranslation;
public record UpdatePostTranslationCommand : IRequest<Unit>
{
    public string? Title { get; init; }

    public string? Context { get; init; }

    public DateTime PublishDate { get; init; }

    public Status Status { get; init; }

    public long ViewCount { get; init; }

    public short LanguageId { get; init; }

    public long NewsId { get; init; }

    public int AuthorId { get; init; }
}

public class UpdatePostTranslationCommandHandler : IRequestHandler<UpdatePostTranslationCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    private readonly IMediator _mediator;

    private readonly IValidator<CreatePostTranslationCommand> _validator;

    public UpdatePostTranslationCommandHandler(
        IApplicationDbContext context,
        IMediator mediator,
        IValidator<CreatePostTranslationCommand> validator)
    {
        _context = context;
        _mediator = mediator;
        _validator = validator;
    }

    public async Task<Unit> Handle(UpdatePostTranslationCommand request, CancellationToken cancellationToken)
    {
        var postTranslation = await _context.PostTranslations
            .FirstOrDefaultAsync(
            pt => pt.LanguageId == request.LanguageId &&
            pt.NewsId == request.NewsId,
            cancellationToken);

        if (postTranslation is null)
        {
            CreatePostTranslationCommand postTranslationCommand = new()
            {
                Title = request.Title,
                Context = request.Context,
                Status = request.Status,
                PublishDate = request.PublishDate,
                ViewCount = request.ViewCount,
                LanguageId = request.LanguageId,
                NewsId = request.NewsId,
                AuthorId = request.AuthorId
            };

            //ValidationResult result = await _validator.ValidateAsync(postTranslationCommand);
           
            await _mediator.Send(postTranslationCommand);
        }
        else
        {
            postTranslation.Title = request.Title;
            postTranslation.Context = request.Context;
            postTranslation.Status = request.Status;
            postTranslation.PublishDate = request.PublishDate;
            postTranslation.ViewCount = request.ViewCount;
            postTranslation.LanguageId = request.LanguageId;
            postTranslation.NewsId = request.NewsId;
            postTranslation.AuthorId = request.AuthorId;

            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}
