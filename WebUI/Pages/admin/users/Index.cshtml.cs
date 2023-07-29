using Application.Common.Models;
using Application.Operations.Roles.Queries.GetRoles;
using Application.Operations.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.user
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public List<UserDto>? Users { get; set; }

        public List<RoleDto>? Roles { get; set; }

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync()
        {
            Users = await _mediator.Send(new GetUsersQuery());

            Roles = await _mediator.Send(new GetRolesQuery());
        }
    }
}
