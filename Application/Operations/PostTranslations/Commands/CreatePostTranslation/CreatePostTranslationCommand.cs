using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.Operations.PostTranslations.Commands.CreatePostTranslation;
public record CreatePostTranslationCommand : IRequest<long>
{
    public string? Title { get; init; }

    public string? Context { get; init; }

    public DateTime? PublishDate { get; init; }

    public Status? Status { get; init; }

    public long ViewCount { get; init; }

    public short? LanguageId { get; init; }

    public long NewsId { get; init; }

    public int? AuthorId { get; init; }
}

public class CreatePostTranslationCommandHandler : IRequestHandler<CreatePostTranslationCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreatePostTranslationCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreatePostTranslationCommand request, CancellationToken cancellationToken)
    {
        var postTranslation = new PostTranslation
        {
            Title = request.Title,
            Context = request.Context,
            InsertDate = DateTime.Now,
            PublishDate = request.PublishDate,
            Status = request.Status,
            ViewCount = request.ViewCount,
            LanguageId = request.LanguageId,
            AuthorId = request.AuthorId,
            NewsId = request.NewsId,
            CreatedBy=request.AuthorId
        };

        _context.PostTranslations.Add(postTranslation);

        postTranslation.AddDomainEvent(new PostTranslationCreatedEvent(postTranslation));

        await _context.SaveChangesAsync(cancellationToken);

        return postTranslation.Id;
    }
}
