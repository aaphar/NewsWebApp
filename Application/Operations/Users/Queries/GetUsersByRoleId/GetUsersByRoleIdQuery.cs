using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Users.Queries.GetUsersByRoleId;
public record GetUsersByRoleIdQuery(int RoleId) : IRequest<List<UserDto>>;

public class GetUsersByRoleIdQueryHandler : IRequestHandler<GetUsersByRoleIdQuery, List<UserDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetUsersByRoleIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetUsersByRoleIdQuery request, CancellationToken cancellationToken)
    {
        var users = await _context.MyUsers
            .Where(u => u.RoleId == request.RoleId)
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<UserDto>>(users);
    }
}