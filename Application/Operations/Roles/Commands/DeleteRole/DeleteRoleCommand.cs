using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Roles.Commands.DeleteRole;
public record DeleteRoleCommand(int Id) : IRequest<Unit>;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Unit> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .Include(l => l.Users)
            .Where(r => r.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (role is null)
        {
            throw new RoleNotFoundException(request.Id);
        }

        // Set the CategoryId to null for related posts
        foreach (var user in role.Users)
        {
            user.RoleId = null;
        }

        _context.Roles.Remove(role);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

