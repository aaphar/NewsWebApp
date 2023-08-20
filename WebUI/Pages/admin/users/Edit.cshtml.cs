using Application.Common.Models;
using Application.Operations.Roles.Queries.GetRoles;
using Application.Operations.Users.Commands.UpdateUser;
using Application.Operations.Users.Queries.GetUserById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.users
{
    [Authorize(Roles = "Adminstrator")]
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public UserDto? UserDto { get; set; }

        [BindProperty]
        public string ImagePath { get; set; }

        public List<RoleDto> Roles { get; set; }


        public long AuthorId { get; set; }

        private readonly UserManager<User> _userManager; // Add UserManager

        public EditModel(IMediator mediator,
            UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;

            Roles = new();
        }

        public async Task OnGetAsync(int id)
        {
            UserDto = await _mediator.Send(new GetUserByIdQuery(id));
            Roles = await _mediator.Send(new GetRolesQuery());

            if (UserDto != null)
            {
                ImagePath = UserDto.ImagePath;
            }

            var loggedInUserId = _userManager.GetUserId(User);
            AuthorId = int.Parse(loggedInUserId); // Convert to int if needed
        }

        public async Task<ActionResult> OnPostAsync(int id)
        {
            // Retrieve the stored image URL from TempData
            var uploadedImagePath = TempData["UploadedImagePath"] as string;

            // If a new image was uploaded, update the ImagePath property
            if (!string.IsNullOrEmpty(uploadedImagePath))
            {
                // Update the ImagePath property with the uploaded image URL
                UserDto.ImagePath = uploadedImagePath;
            }

            // If no new image was uploaded, keep the existing ImagePath value
            else if (string.IsNullOrEmpty(UserDto.ImagePath))
            {
                UserDto.ImagePath = ImagePath;
            }

            UpdateUserCommand updateUserCommand = new()
            {
                Id = id,
                Email = UserDto.Email,
                Password = UserDto.Password,
                Name = UserDto?.Name?.Substring(0, 1).ToUpper() + UserDto?.Name?.Substring(1).ToLower(),
                Surname = UserDto?.Surname?.Substring(0, 1).ToUpper() + UserDto?.Surname?.Substring(1).ToLower(),
                ImagePath = UserDto?.ImagePath,
                RoleId = UserDto.RoleId,
                AuthorId = AuthorId
            };

            TempData["UploadedImagePath"] = null;

            await _mediator.Send(updateUserCommand);

            return RedirectToPage("/admin/users/detail", new { id });
        }
    }
}
