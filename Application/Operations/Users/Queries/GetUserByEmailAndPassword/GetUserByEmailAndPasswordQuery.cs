using Application.Common.Interfaces;
using Application.Common.Models;
using AutoMapper;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Users.Queries.GetUserByEmailAndPassword;
public record GetUserByEmailAndPasswordQuery : IRequest<UserDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}


public class GetUserByEmailAndPasswordQueryHandler : IRequestHandler<GetUserByEmailAndPasswordQuery, UserDto>
{
    private readonly IApplicationDbContext _context;

    private readonly IMapper _mapper;

    public GetUserByEmailAndPasswordQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserByEmailAndPasswordQuery request, CancellationToken cancellationToken)
    {
        var emailWithUser = await _context.Users
            .FirstOrDefaultAsync(e => e.Email == request.Email);

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email
            && u.Password == request.Password);

        if (emailWithUser != null)
        {
            if (emailWithUser.Password != request.Password)
            {
                throw new PasswordNotMatchException(request.Password);
            }
        }
        else
        {
            throw new EmailNotFoundException(request.Email);
        }

        var userDto = _mapper.Map<UserDto>(user);

        return userDto;
    }
}
