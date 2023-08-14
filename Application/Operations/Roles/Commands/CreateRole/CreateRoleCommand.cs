using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Operations.Roles.Commands.CreateRole;
public record CreateRoleCommand : IRequest<int>
{
    public string? Name { get; init; }
}

public class CreateRoleCommandHandler:IRequestHandler<CreateRoleCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public CreateRoleCommandHandler(IApplicationDbContext context,
        UserManager<User> userManager,
        RoleManager<Role> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role
        {
            Name = request.Name,
            NormalizedName = request.Name.ToUpper(),
        };

        var result = await _roleManager.CreateAsync(role);

        if(result.Succeeded)
        {
            await _context.SaveChangesAsync(cancellationToken);

            return role.Id;
        }
        else
        {
            throw new Exception(result.Errors.ToString());
        }
    }
}