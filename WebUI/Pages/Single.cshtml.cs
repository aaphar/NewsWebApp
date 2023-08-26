using Application.Common.Models;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.Categories.Queries.GetCategoryById;
using Application.Operations.Hashtags.Queries.GetHashtags;
using Application.Operations.PostHashtag.Queries.GetPostHashtagsByPostId;
using Application.Operations.Posts.Queries.GetPostTranslationByTitle;
using Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageCodeAndNewsId;
using Application.Operations.Users.Queries.GetUserById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages
{
    public class SingleModel : PageModel
    {
        private readonly IMediator _mediator;

        public PostDto? Post { get; set; }

        public PostTranslationDto? PostTranslation { get; set; }

        public UserDto? User { get; set; }

        public List<CategoryDto>? Categories { get; set; }

        [BindProperty]
        public string? CategoryTitle { get; set; }

        public List<PostHashtagDto>? PostHashtags { get; set; }

        public List<HashtagDto>? Hashtags { get; set; }

        public HashtagDto? Hashtag { get; set; }

        public SingleModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGetAsync(string title)
        {
            
            Post = await _mediator.Send(new GetPostByTitleQuery(title));

            if (Post.CategoryId != null)
            {
                CategoryDto category = await _mediator.Send(new GetCategoryByIdQuery() { Id = (short)Post.CategoryId });

                CategoryTitle = category.Description;
            }
            else
            {
                CategoryTitle = null;
            }

            PostTranslation = await _mediator.Send(new GetPostTranslationByLanguageCodeAndNewsIdQuery() { NewsId = Post.Id, LanguageCode = "en" });

            Categories = await _mediator.Send(new GetCategoriesQuery());

            PostHashtags = await _mediator.Send(new GetPostHashtagsByPostIdQuery(PostTranslation.Id));

            Hashtags = await _mediator.Send(new GetHashtagsQuery());

            int userId; // This variable will hold the actual user ID

            if (PostTranslation.AuthorId.HasValue)
            {
                userId = PostTranslation.AuthorId.Value; // Extract the value from the nullable int
            }
            else
            {
                userId = -1; // For example, using -1 as a placeholder
            }

            User = await _mediator.Send(new GetUserByIdQuery(userId));
        }
    }
}
