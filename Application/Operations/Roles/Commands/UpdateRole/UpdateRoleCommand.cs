using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.Operations.Roles.Commands.UpdateRole;
public record UpdateRoleCommand : IRequest<Unit>
{
    public int Id { get; init; }
    public string? Name { get; init; }
}

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (role is null)
        {
            throw new RoleNotFoundException(request.Id);
        }

        role.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}