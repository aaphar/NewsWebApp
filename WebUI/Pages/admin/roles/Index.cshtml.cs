using Application.Common.Models;
using Application.Operations.Roles.Commands.CreateRole;
using Application.Operations.Roles.Commands.DeleteRole;
using Application.Operations.Roles.Queries.GetRoles;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.roles;
public class RoleModel : PageModel
{
    private readonly IMediator _mediator;

    private readonly IValidator<CreateRoleCommand> _validator;

    [BindProperty]
    public string? Title { get; init; }

    public List<RoleDto>? Roles { get; set; }

    public RoleDto? RoleDto { get; set; }

    public RoleModel(
        IMediator mediator,
        IValidator<CreateRoleCommand> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }
    public async Task OnGetAsync()
    {
        Roles = await _mediator.Send(new GetRolesQuery());//sdkjsk
    }
        
    public async Task<IActionResult> OnPostDeleteAsync(int Id)
    {
        await _mediator.Send(new DeleteRoleCommand(Id));

        string _message = $"Role with ID = {Id} was successfully deleted";
        await Console.Out.WriteLineAsync(_message);

        return RedirectToPage("/admin/roles/index");
    }
}


