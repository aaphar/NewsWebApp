using Application.Common.Models;
using Application.Operations.Roles.Queries.GetRoles;
using Application.Operations.Users.Commands.CreateUser;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.user
{
    [Authorize(Roles = "Adminstrator")]
    public class AddModel : PageModel
    {
        private readonly IMediator _mediator;

        private readonly IValidator<CreateUserCommand> _validator;

        [BindProperty]
        public string? UserName { get; set; }

        [BindProperty]
        public string? Password { get; set; }

        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public string? Surname { get; set; }

        [BindProperty]
        public string? Email { get; set; }

        [BindProperty]
        public short RoleId { get; set; }

        [BindProperty]
        public string? ImagePath { get; set; }

        public List<RoleDto>? Roles { get; set; }

        public AddModel(IMediator mediator,
            IValidator<CreateUserCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }
        public async Task OnGetAsync()
        {
            Roles = await _mediator.Send(new GetRolesQuery());
        }

        public async Task<ActionResult> OnPostAsync()
        {
            var uploadedImagePath = TempData["UploadedImagePath"] as string;
            if (!string.IsNullOrEmpty(uploadedImagePath))
            {
                // Update the ImagePath property with the uploaded image URL
                ImagePath = uploadedImagePath;
            }

            CreateUserCommand createUserCommand = new()
            {
                Name = Name?.Substring(0, 1).ToUpper() + Name?.Substring(1).ToLower(),
                Surname = Surname?.Substring(0, 1).ToUpper() + Surname?.Substring(1).ToLower(),
                Email = Email,
                Password = Password,
                ImagePath = ImagePath ?? null,
                RoleId = RoleId
            };

            ValidationResult result = await _validator.ValidateAsync(createUserCommand);

            int id = await _mediator.Send(createUserCommand);

            TempData["UploadedImagePath"] = null;

            return RedirectToPage("/admin/users/detail", new { id });
            //try
            //{
                
            //}
            //catch (Exception ex)
            //{
            //    TempData["ErrorMessage"] = ex.Message;
            //    return new RedirectToPageResult("/admin/error");
            //}

        }
    }
}
