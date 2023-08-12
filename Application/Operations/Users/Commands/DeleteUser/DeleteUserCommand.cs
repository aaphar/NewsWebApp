using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Users.Commands.DeleteUser;
public record DeleteUserCommand(int Id):IRequest<Unit>;


public class DeleteUserCommandHandler:IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(l => l.PostTranslations)
            .Where(u => u.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if(user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        // Set the CategoryId to null for related posts
        foreach (var post in user.PostTranslations)
        {
            post.AuthorId = null;
        }

        _context.Users.Remove(user);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

