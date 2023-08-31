﻿using Application.CommandQueries.Language.Queries.GetLanguages;
using Application.Common.Models;
using Application.Operations.Posts.Queries.GetPosts;
using Application.Operations.PostTranslations.Queries.GetPostSearchsByTitle;
using Application.Operations.PostTranslations.Queries.GetPostTranslationByLanguageCodeAndNewsId;
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

        public List<PostDto>? LastlyAddedPosts { get; set; }

        public List<PostDto>? Posts { get; set; }

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnGet()
        {
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
                }
                catch (Exception e)
                {
                    LastlyAddedPosts.RemoveAt(i);
                }
            }
        }
    }
}