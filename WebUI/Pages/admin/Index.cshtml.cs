using Application.Common.Behaviours;
using Application.Common.Results;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.authentication
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }


        private readonly IMediator _mediator;
        private readonly SignInManager<User> _signInManager;

        public LoginModel(IMediator mediator, 
            SignInManager<User> signInManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
        }

        public async Task<ActionResult> OnPostSignInAsync()
        {
            try
            {
                SignInCommand signInCommand = new SignInCommand()
                {
                    Email = Email,  
                    Password = Password,
                };

                var result = await _mediator.Send(signInCommand);

                if (!result.Succeeded)
                {
                    TempData["ErrorMessage"] = result.ToString();
                    return new RedirectToPageResult("/admin/error");
                }

                return new RedirectToPageResult("/admin/posts/index");

            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return new RedirectToPageResult("/admin/error");
            }
        }
    }
}
