using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Roles.Commands.DeleteRole;
public record DeleteRoleCommand(short Id) : IRequest<Unit>;

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
            .Where(r => r.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (role is null)
        {
            throw new RoleNotFoundException(request.Id);
        }

        _context.Roles.Remove(role);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

