﻿using Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.Roles.Commands.UpdateRole;
public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateRoleCommandValidator(IApplicationDbContext context)
    {
        _context=context;

        RuleFor(r => r.Name)
                .MaximumLength(25)
                .NotEmpty()
                .MustAsync(BeUniqueTitle)
                .WithMessage("The specified title already exists.");
    }

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Roles
            .AllAsync(l => l.Name != title, cancellationToken);
    }
}

