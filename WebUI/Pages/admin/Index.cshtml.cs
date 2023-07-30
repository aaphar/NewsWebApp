using Application.Common.Models;
using Application.Operations.Users.Queries.GetUserByEmailAndPassword;
using Application.Operations.Users.Queries.GetUsers;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebUI.Pages.admin
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string? Email { get; init; }

        [BindProperty]
        public string? Password { get; init; }

        public List<UserDto>? Users { get; set; }

        private readonly IMediator _mediator;

        public LoginModel(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                UserDto user = await _mediator.Send(new GetUserByEmailAndPasswordQuery()
                {
                    Email = Email,
                    Password = Password
                });
                                              

                return RedirectToPage("/admin/roles/index");
            }
            catch (EmailNotFoundException ex)
            {
                // Handle email not found exception
                ModelState.AddModelError(nameof(Email), ex.Message);
                return Page();
            }
            catch (PasswordNotMatchException ex)
            {
                // Handle password not matched exception
                ModelState.AddModelError(nameof(Password), ex.Message);
                return Page();
            }
            catch(UserNotFoundException ex)
            {
                ModelState.AddModelError(nameof(Password), ex.Message);
                return Page();
            }

        }

        private UserDto GetUserByEmailAndPassword(string email, string password)
        {
            UserDto logUser = null;

            foreach (var user in Users)
            {
                if (user.Email == email && user.Password == password)
                {
                    logUser = user;
                }
                if (user.Email != email)
                {
                    throw new EmailNotFoundException(email);
                }
                else if (user.Email == email && user.Password != password)
                {
                    throw new PasswordNotMatchException(password);
                }
            }

            return logUser;
        }
    }
}
