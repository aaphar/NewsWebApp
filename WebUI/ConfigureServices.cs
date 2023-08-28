﻿using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace WebUI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebUIServices(IServiceCollection services)
        {
            // Register application services
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IPostService, PostService>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            // Other dependencies specific to the presentation layer
            services.AddHttpContextAccessor();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddRazorPages(options =>
            {
                options.Conventions.AddPageRoute("/Single", "{title}");
                options.Conventions.AddPageRoute("/Blog", "{lang}/Blog");

            });

            

            services.AddControllers();

            return services;
        }

    }
}
