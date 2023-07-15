using Application.Common.Interfaces;
using Domain.Events;
using MediatR;

namespace Application.CommandQueries.Language.Commands.CreateLanguage;
public record CreateLanguageCommand : IRequest<short>
{
    public string? Title { get; init; }
    public string? Code { get; init; }
}

public class CreateLanguageCommandHandler : IRequestHandler<CreateLanguageCommand, short>
{
    private readonly IApplicationDbContext _context;

    public CreateLanguageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<short> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Language
        {
            LanguageCode = request.Code,
            Title = request.Title,
        };

        entity.AddDomainEvent(new LanguageCreatedEvent(entity));
        _context.Languages.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

