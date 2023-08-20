using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Operations.Roles.Commands.CreateRole;
public record CreateRoleCommand : IRequest<int>
{
    public string? Name { get; init; }
    public long AuthorId { get; set; }
}

public class CreateRoleCommandHandler:IRequestHandler<CreateRoleCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly RoleManager<Role> _roleManager;

    public CreateRoleCommandHandler(IApplicationDbContext context,
        RoleManager<Role> roleManager)
    {
        _context = context;
        _roleManager = roleManager;
    }

    public async Task<int> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new Role
        {
            Name = request.Name,
            NormalizedName = request.Name.ToUpper(),


            Created = DateTime.Now,
            CreatedBy = request.AuthorId
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