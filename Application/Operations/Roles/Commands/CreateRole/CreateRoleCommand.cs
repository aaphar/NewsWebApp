using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Operations.Roles.Commands.CreateRole;
public record CreateRoleCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateRoleCommandHandler:IRequestHandler<CreateRoleCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role
        {
            Title = request.Title,
        };
        
        _context.MyRoles.Add(role);

        await _context.SaveChangesAsync(cancellationToken);
        
        role.AddDomainEvent(new RoleCreatedEvent(role));

        return role.Id;
    }
}