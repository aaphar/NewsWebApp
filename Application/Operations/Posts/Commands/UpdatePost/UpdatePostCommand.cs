using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Operations.Posts.Commands.UpdatePost;
public record UpdatePostCommand : IRequest<Unit>
{
    public long Id { get; init; }

    public string? Title { get; init; }

    public string? ImagePath { get; init; }

    public DateTime? PublishDate { get; init; }

    public short? CategoryId { get; init; }
    public long AuthorId { get; set; }
}


public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdatePostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (post is null)
        {
            throw new PostNotFoundException(request.Id);
        }

        post.Title = request.Title;
        post.ImagePath = request.ImagePath;
        post.PublishDate = request.PublishDate;
        post.CategoryId = request.CategoryId;

        post.LastModified = DateTime.Now;
        post.LastModifiedBy = request.AuthorId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}