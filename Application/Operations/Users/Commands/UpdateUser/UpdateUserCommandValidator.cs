using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Users.Commands.UpdateUser;
public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateUserCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(u => u.UserName)
            .NotEmpty()
            .MustAsync(BeUniqueUserName)
            .WithMessage("The specified username already exists.");

        RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(8);

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
        return await _context.MyRoles.AnyAsync(l => l.Id == roleId, cancellationToken);
    }

    private async Task<bool> BeUniqueUserName(string userName, CancellationToken cancellationToken)
    {
        return await _context.MyUsers
            .AllAsync(l => l.UserName != userName, cancellationToken);
    }

    private async Task<bool> BeUniqueEmail(string? email, CancellationToken cancellationToken)
    {
        return await _context.MyUsers
            .AllAsync(l => l.Email != email, cancellationToken);
    }
}

