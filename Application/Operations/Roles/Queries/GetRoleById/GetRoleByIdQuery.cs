using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Exceptions;
using MediatR;

namespace Application.Operations.Roles.Queries.GetRoleById;
public record GetRoleByIdQuery : IRequest<RoleDto>
{
    public short Id { get; init; }
}

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetRoleByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RoleDto> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _context.Roles
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (role is null)
        {
            throw new RoleNotFoundException(request.Id);
        }

        var roleDto = _mapper.Map<RoleDto>(role);

        return roleDto;
    }
}

