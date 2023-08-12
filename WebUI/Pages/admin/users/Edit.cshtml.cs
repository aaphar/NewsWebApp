using Application.Common.Models;
using Application.Operations.Roles.Queries.GetRoles;
using Application.Operations.Users.Commands.UpdateUser;
using Application.Operations.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.users
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public UserDto? UserDto { get; set; }

        public List<RoleDto> Roles { get; set; }

        public EditModel(IMediator mediator)
        {
            _mediator = mediator;

            Roles = new();

        }
        public async Task OnGetAsync(int id)
        {
            UserDto = await _mediator.Send(new GetUserByIdQuery(id));
            Roles = await _mediator.Send(new GetRolesQuery());

        }

        public async Task<ActionResult> OnPostAsync(int id)
        {
            UpdateUserCommand updateUserCommand = new()
            {
                Id = id,
                UserName = UserDto.UserName,
                Email = UserDto.Email,
                Password = UserDto.Password,
                Name = UserDto?.Name?.Substring(0, 1).ToUpper() + UserDto?.Name?.Substring(1).ToLower(),
                Surname = UserDto?.Surname?.Substring(0, 1).ToUpper() + UserDto?.Surname?.Substring(1).ToLower(),
                RoleId=UserDto.RoleId
            };

            await _mediator.Send(updateUserCommand);

            return RedirectToPage("/admin/users/detail", new { id });
        }
    }
}
