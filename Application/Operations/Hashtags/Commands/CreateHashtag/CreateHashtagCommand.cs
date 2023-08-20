using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Operations.Hashtags.Commands.CreateHashtag;

public record CreateHashtagCommand : IRequest<long>
{
    public string? Title { get; init; }
    public long AuthorId { get; set; }
}


public class CreateHashtagCommandHandler : IRequestHandler<CreateHashtagCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreateHashtagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateHashtagCommand request, CancellationToken cancellationToken)
    {
        var hashtag = new Hashtag
        {
            Title = request.Title,
            Created = DateTime.Now,
            CreatedBy = request.AuthorId
        };

        _context.Hashtags.Add(hashtag);

        await _context.SaveChangesAsync(cancellationToken);

        return hashtag.Id;
    }
}