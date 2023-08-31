using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Categories.Queries.GetCategories;
using Application.Operations.Posts.Queries.GetPosts;
using Application.Operations.Posts.Queries.GetPostsByCategoryId;
using Application.Operations.PostTranslations.Queries.GetPostSearchsByTitle;
using Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageCodeAndNewsId;
using Application.Operations.Users.Queries.GetUserById;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace WebUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public List<LanguageDto>? Languages { get; set; }

        [BindProperty]
        public string? SelectedLanguageCode { get; set; }

        [BindProperty]
        public string? SearchTerm { get; set; }

        public List<PostDto>? Posts { get; set; }

        public List<PostDto>? LastlyAddedPosts { get; set; }

        public List<CategoryDto>? Categories { get; set; }

        public UserDto? User { get; set; }

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGet()
        {
            Categories = await _mediator.Send(new GetCategoriesQuery());

            Languages = await _mediator.Send(new GetLanguagesQuery());

            // Get the language code from the URL path
            var pathSegments = HttpContext.Request.Path.Value.Split('/');
            var languageFromUrl = pathSegments.Length > 2 ? pathSegments[1] : null;

            var languageFromSession = HttpContext.Session.GetString("SelectedLanguage");

            var language = !string.IsNullOrEmpty(languageFromUrl) ? languageFromUrl : languageFromSession;

            SelectedLanguageCode = language;

            Posts = await _mediator.Send(new GetPostsQuery());

            LastlyAddedPosts = Enumerable.Reverse(Posts).ToList(); ;

            // Fetch post translations for each post
            for (int i = LastlyAddedPosts.Count - 1; i >= 0; i--)
            {
                var post = LastlyAddedPosts[i];
                try
                {
                    post.PostTranslation = await _mediator.Send(new GetPostTranslationByLanguageCodeAndNewsIdQuery
                    {
                        LanguageCode = language != null ? language : SelectedLanguageCode,
                        NewsId = post.Id
                    });

                    int userId; // This variable will hold the actual user ID

                    if (post.PostTranslation.AuthorId.HasValue)
                    {
                        userId = post.PostTranslation.AuthorId.Value; // Extract the value from the nullable int
                    }
                    else
                    {
                        userId = -1; // For example, using -1 as a placeholder
                    }

                    post.PostTranslation.User = await _mediator.Send(new GetUserByIdQuery(userId));

                    Console.WriteLine(post.PostTranslation.User.Name);
                }
                catch (Exception e)
                {
                    LastlyAddedPosts.RemoveAt(i);
                }
            }

            for (int i = 0; i < Categories.Count; i++)
            {
                var category = Categories[i];

                try
                {
                    category.Posts = await _mediator.Send(new GetPostsByCategoryIdQuery(category.Id));

                    category.Posts= Enumerable.Reverse(category.Posts).ToList(); 

                    var postList = new List<PostDto>(category.Posts);

                    for (int j = postList.Count - 1; j >= 0; j--)
                    {
                        var post = postList[j];

                        try
                        {
                            post.PostTranslation = await _mediator.Send(new GetPostTranslationByLanguageCodeAndNewsIdQuery
                            {
                                LanguageCode = language != null ? language : SelectedLanguageCode,
                                NewsId = post.Id
                            });

                            int userId; // This variable will hold the actual user ID

                            if (post.PostTranslation.AuthorId.HasValue)
                            {
                                userId = post.PostTranslation.AuthorId.Value; // Extract the value from the nullable int
                            }
                            else
                            {
                                userId = -1; // For example, using -1 as a placeholder
                            }

                            post.PostTranslation.User = await _mediator.Send(new GetUserByIdQuery(userId));

                            Console.WriteLine(post.PostTranslation.User.Name);
                        }
                        catch (Exception e)
                        {
                            postList.RemoveAt(j);
                        }
                    }

                    category.Posts = postList;

                }
                catch (Exception e)
                {
                    Categories.RemoveAt(i);
                }
            }

        }
    }
}