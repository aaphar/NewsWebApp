using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.CommandQueries.Language.Commands.UpdateLanguage;
public record UpdateLanguageCommand : IRequest<Unit>
{
    public short Id { get; init; }
    public string? Title { get; init; }
    public string? Code { get; init; }
    public long AuthorId { get; set; }

}

public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateLanguageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Unit> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = await _context.Languages
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (language is null)
        {
            throw new LanguageNotFoundException(request.Id);
        }

        language.Title = request.Title;
        language.LanguageCode = request.Code;

        language.LastModified = DateTime.Now;
        language.LastModifiedBy = request.AuthorId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}