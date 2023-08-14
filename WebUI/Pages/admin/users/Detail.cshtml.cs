using Application.Common.Models;
using Application.Operations.Roles.Commands.DeleteRole;
using Application.Operations.Roles.Queries.GetRoles;
using Application.Operations.Users.Commands.DeleteUser;
using Application.Operations.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.users;

[Authorize(Roles = "Adminstrator")]
public class DetailModel : PageModel
{
    private readonly IMediator _mediator;

    public UserDto? User { get; set; }

    public List<RoleDto>? Roles { get; set; }

    public DetailModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task OnGetAsync(int id)
    {
        GetUserByIdQuery getUserByIdQuery = new GetUserByIdQuery(id);

        User = await _mediator.Send(getUserByIdQuery);

        Roles = await _mediator.Send(new GetRolesQuery());
    }

    public async Task<IActionResult> OnPostDeleteAsync(int Id)
    {
        await _mediator.Send(new DeleteUserCommand(Id));

        string _message = $"User with Id = {Id} was successfully deleted";

        await Console.Out.WriteLineAsync(_message);

        return RedirectToPage("/admin/users/index");
    }
}

