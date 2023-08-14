using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Operations.Users.Commands.CreateUser;
public record CreateUserCommand : IRequest<int>
{
    public string? Name { get; init; }

    public string? Surname { get; init; }

    public string? Email { get; init; }

    public string Password { get; init; }

    public string? ImagePath { get; init; }

    public int? RoleId { get; init; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public CreateUserCommandHandler(IApplicationDbContext context,
        UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = request.Name,
            Surname = request.Surname,
            UserName = request.Email,
            Email = request.Email,
            Password = request.Password,
            ImagePath = request.ImagePath,
            RoleId = request.RoleId,
            SecurityStamp = Guid.NewGuid().ToString() // Set a new security stamp value
        };


        var result = await _userManager.CreateAsync(user, request.Password);

        Console.WriteLine(result.Errors);

        if (result.Succeeded)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());

            if (role != null)
            {
                var resultRole = await _userManager.AddToRoleAsync(user, role.Name);
                if (!resultRole.Succeeded)
                {
                    // Handle role assignment error
                }
            }

            else
            {
                throw new RoleNotFoundException( request.RoleId);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return user.Id;
        }
        else
        {
            throw new Exception(result.Errors.ToString());
        }

        //_context.Users.Add(user);

        //await _context.SaveChangesAsync(cancellationToken);

        //return user.Id;
    }
}
