using Application.CommandQueries.Language.Commands.CreateLanguage;
using Application.Common.Interfaces;
using Domain.Events;
using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CommandQueries.Language.Commands.UpdateLanguage;
public record UpdateLanguageCommand : IRequest<short>
{
    public short Id { get; set; }
    public string? Title { get; init; }
    public string? Code { get; init; }
}

public class UpdateLanguageCommandHandler : IRequestHandler<UpdateLanguageCommand, short>
{
    private readonly IApplicationDbContext _context;

    public UpdateLanguageCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<short> Handle(UpdateLanguageCommand request, CancellationToken cancellationToken)
    {
        var language = await _context.Languages
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (language is null)
        {
            throw new LanguageNotFoundException(request.Id);
        }

        language.Title = request.Title;
        language.LanguageCode = request.Code;

        await _context.SaveChangesAsync(cancellationToken);

        return language.Id;
    }
}