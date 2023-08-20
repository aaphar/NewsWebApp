using Application.CommandQueries.Language.Commands.UpdateLanguage;
using Application.Common.Models;
using Application.Operations.Language.Queries.GetLanguageById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.language
{
    [Authorize(Roles = "Adminstrator")]
    public class EditModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public LanguageDto? LanguageDto { get; set; }
        public long AuthorId { get; set; }

        private readonly UserManager<User> _userManager; // Add UserManager

        public EditModel(
                IMediator mediator,
                UserManager<User> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task OnGetAsync(short id)
        {
            LanguageDto = await _mediator.Send(new GetLanguageByIdQuery() { Id = id });   // burda id=0;
            var loggedInUserId = _userManager.GetUserId(User);
            AuthorId = int.Parse(loggedInUserId); // Convert to int if needed
        }


        public async Task<ActionResult> OnPostAsync(short id)
        {
            UpdateLanguageCommand updateLanguageCommand = new()
            {
                Id = id,
                Title = LanguageDto?.Title?.Substring(0, 1).ToUpper() + LanguageDto?.Title?.Substring(1).ToLower(),
                Code = LanguageDto?.LanguageCode?.ToLower(),
                AuthorId = AuthorId
            };


            await _mediator.Send(updateLanguageCommand);
            string _message = $"Language with ID = {id} was successfully updated";
            await Console.Out.WriteLineAsync(_message);

            return RedirectToPage("/admin/languages/detail", new { id });
        }

    }
}
