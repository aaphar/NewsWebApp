using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Users.Queries.GetUsers;
public record GetUsersQuery : IRequest<List<UserDto>>;

public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetUsersQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _context.MyUsers
            .ToListAsync(cancellationToken);

        var usersDtos = _mapper.Map<List<UserDto>>(users);

        return usersDtos;
    }
}

