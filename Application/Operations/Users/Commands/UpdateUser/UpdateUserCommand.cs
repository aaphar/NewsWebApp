using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Operations.Users.Commands.UpdateUser;
public record UpdateUserCommand : IRequest<Unit>
{
    public int Id { get; init; }

    public string? Name { get; init; }

    public string? Surname { get; init; }

    public string? Email { get; init; }

    public string Password { get; init; }

    public string? ImagePath { get; init; }

    public int? RoleId { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;


    public UpdateUserCommandHandler(IApplicationDbContext context, 
        UserManager<User> userManager, 
        RoleManager<Role> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }


    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.Id);

        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        user.Password = request.Password;
        user.Name = request.Name;
        user.ImagePath = request.ImagePath;
        user.Surname = request.Surname;
        user.UserName = request.Email;
        user.Email = request.Email;

        if (user.RoleId != request.RoleId)
        {
            var newRole = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            
            if (newRole != null)
            {
                // Remove existing roles
                var existingRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, existingRoles);

                // Add new role
                user.RoleId = request.RoleId;
                await _userManager.AddToRoleAsync(user, newRole.Name);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}