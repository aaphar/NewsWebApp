using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Users.Commands.CreateUser;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandValidator(IApplicationDbContext context)
    {
        _context = context;
     
        RuleFor(v => v.Password)
           .MinimumLength(8)
           .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
           .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter")
           .Matches("[0-9]").WithMessage("Password must contain at least one digit")
           .Matches("[!@#$%^&*(),.?\":{}|<>]").WithMessage("Password must contain at least one special character")
           .NotEmpty();

        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .MustAsync(BeUniqueEmail)
            .WithMessage("The specified email address already exists.");

        RuleFor(u => u.Name)
            .NotEmpty();

        RuleFor(u => u.Surname)
            .NotEmpty();

        RuleFor(u => u.RoleId)
            .NotEmpty()
            .MustAsync(RoleExists)
            .WithMessage("RoleId does not exist in the database");
    }

    private async Task<bool> RoleExists(int? roleId, CancellationToken cancellationToken)
    {
        return await _context.Roles.AnyAsync(l => l.Id == roleId, cancellationToken);
    }

    private async Task<bool> BeUniqueUserName(string userName, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AllAsync(l => l.UserName != userName, cancellationToken);
    }
    
    private async Task<bool> BeUniqueEmail(string? email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AllAsync(l => l.Email != email, cancellationToken);
    }
}

