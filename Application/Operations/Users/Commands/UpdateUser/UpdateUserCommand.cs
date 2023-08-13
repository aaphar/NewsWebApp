using Application.Common.Interfaces;
using Domain.Exceptions;
using MediatR;

namespace Application.Operations.Users.Commands.UpdateUser;
public record UpdateUserCommand : IRequest<Unit>
{
    public int Id { get; init; }

    public string? Name { get; init; }

    public string? Surname { get; init; }

    public string? Email { get; init; }

    public string Password { get; init; }

    public int? RoleId { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        user.Password = request.Password;
        user.Name = request.Name;
        user.Surname = request.Surname;
        user.Email = request.Email;
        user.RoleId = request.RoleId;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}