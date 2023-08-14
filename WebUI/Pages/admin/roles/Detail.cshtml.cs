using Application.Common.Models;
using Application.Operations.Roles.Commands.DeleteRole;
using Application.Operations.Roles.Queries.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.roles
{
    [Authorize(Roles = "Adminstrator")]
    public class DetailModel : PageModel
    {
        private readonly IMediator _mediator;
        public RoleDto? Role { get; set; }

        public DetailModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task OnGetAsync(short id)
        {
            GetRoleByIdQuery getRoleByIdQuery = new()
            {
                Id = id
            };

            Role = await _mediator.Send(getRoleByIdQuery);
        }

        public async Task<IActionResult> OnPostDeleteAsync(short Id)
        {
            await _mediator.Send(new DeleteRoleCommand(Id));

            string _message = $"Role with Id = {Id} was successfully deleted";

            await Console.Out.WriteLineAsync(_message);

            return RedirectToPage("/admin/roles/index");
        }
    }
}
