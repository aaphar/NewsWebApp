using Application.CommandQueries.Language.Commands.UpdateLanguage;
using Application.Common.Models;
using Application.Operations.Roles.Commands.UpdateRole;
using Application.Operations.Roles.Queries.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.roles
{
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public RoleDto? RoleDto { get; set; }

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;

        }

        public async Task OnGetAsync(short id)
        {
            RoleDto = await _mediator.Send(new GetRoleByIdQuery() { Id = id });   // burda id=0;

        }

        public async Task<ActionResult> OnPostAsync(short id)
        {
            UpdateRoleCommand updateRoleCommand = new()
            {
                Id = id,
                Title = RoleDto?.Title,
            };


            await _mediator.Send(updateRoleCommand);
            string _message = $"Role with ID = {id} was successfully updated";
            await Console.Out.WriteLineAsync(_message);

            return RedirectToPage("/admin/roles/detail", new { id });
        }

    }
}
