using Application.CommandQueries.Language.Commands.UpdateLanguage;
using Application.Common.Models;
using Application.Operations.Roles.Commands.UpdateRole;
using Application.Operations.Roles.Queries.GetRoleById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.roles
{
    [Authorize(Roles = "Adminstrator")]
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public RoleDto? RoleDto { get; set; }


        public long AuthorId { get; set; }

        private readonly UserManager<User> _userManager; // Add UserManager

        public EditModel(IMediator mediator,
            UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;

        }

        public async Task OnGetAsync(short id)
        {
            RoleDto = await _mediator.Send(new GetRoleByIdQuery() { Id = id });   // burda id=0;
            
            var loggedInUserId = _userManager.GetUserId(User);
            AuthorId = int.Parse(loggedInUserId); // Convert to int if needed
        }

        public async Task<ActionResult> OnPostAsync(short id)
        {
            UpdateRoleCommand updateRoleCommand = new()
            {
                Id = id,
                Name = RoleDto?.Name,
                AuthorId = AuthorId
            };


            await _mediator.Send(updateRoleCommand);
            string _message = $"Role with ID = {id} was successfully updated";
            await Console.Out.WriteLineAsync(_message);

            return RedirectToPage("/admin/roles/detail", new { id });
        }

    }
}
