using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Roles.Queries.GetRoles;
public record GetRolesQuery : IRequest<List<RoleDto>>;

public sealed class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetRolesQueryHandler(
        IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await _context.Roles
            .ToListAsync(cancellationToken);
                
        var roleDto = _mapper.Map<List<RoleDto>>(roles);

        return roleDto;
    }
}