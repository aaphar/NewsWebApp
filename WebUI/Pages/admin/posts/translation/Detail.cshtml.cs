using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.CategoryTranslations.Commands.DeleteCategoryTranslation;
using Application.Operations.Posts.Queries.GetPosts;
using Application.Operations.PostTranslations.Commands.DeletePostTranslation;
using Application.Operations.PostTranslations.Queries.GetPostTranslationById;
using Application.Operations.Users.Queries.GetUsers;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.admin.posts.translation
{
    public class DetailModel : PageModel
    {
        private readonly IMediator _mediator;

        [BindProperty]
        public PostTranslationDto? Translation { get; set; }

        public long NewsId { get; set; }


        public List<LanguageDto>? Languages { get; set; }

        public List<PostDto>? Posts { get; set; }

        public List<UserDto>? Users { get; set; }

        public DetailModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync(long id)
        {
            Posts = await _mediator.Send(new GetPostsQuery());

            //get languages
            Languages = await _mediator.Send(new GetLanguagesQuery());

            Users = await _mediator.Send(new GetUsersQuery());

            Translation = await _mediator.Send(new GetPostTranslationByIdQuery(id));
        }


        public async Task<IActionResult> OnPostDeleteAsync(short Id)
        {
            await _mediator.Send(new DeletePostTranslationCommand(Id));

            string _message = $"Translation with Id = {Id} was successfully deleted";

            await Console.Out.WriteLineAsync(_message);

            // redirect problem

            return RedirectToPage("/admin/posts/detail", new { Id = NewsId });

        }
    }
}
