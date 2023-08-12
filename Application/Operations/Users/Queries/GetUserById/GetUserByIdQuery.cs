using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Exceptions;
using MediatR;

namespace Application.Operations.Users.Queries.GetUserById;
public record GetUserByIdQuery(int Id) : IRequest<UserDto>;

public class GetUserByIdQueryHandler:IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if(user is null)
        {
            throw new UserNotFoundException(request.Id);
        }

        var userDto = _mapper.Map<UserDto>(user);

        return userDto;
    }
}