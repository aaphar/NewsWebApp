using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.Operations.Users.Commands.CreateUser;
public record CreateUserCommand : IRequest<int>
{
    public string UserName { get; init; }

    public string Password { get; init; }

    public string? Name { get; init; }

    public string? Surname { get; init; }

    public string? Email { get; init; }

    public int? RoleId { get; init; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            UserName = request.UserName,
            Password = request.Password,
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            RoleId = request.RoleId,
        };

        user.AddDomainEvent(new UserCreatedEvent(user));
        _context.MyUsers.Add(user);

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
