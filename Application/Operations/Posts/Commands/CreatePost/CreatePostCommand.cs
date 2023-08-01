using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Operations.Posts.Commands.CreatePost;
public record CreatePostCommand : IRequest<long>
{
    public DateTime? PublishDate { get; init; }

    public short CategoryId { get; init; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, long>
{
    private readonly IApplicationDbContext _context;

    public CreatePostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post
        {
            PublishDate = request.PublishDate,
            InsertDate = DateTime.Now,
            CategoryId = request.CategoryId
        };

        _context.Posts.Add(post);

        post.AddDomainEvent(new PostCreatedEvent(post));

        await _context.SaveChangesAsync(cancellationToken);

        return post.Id;
    }
}