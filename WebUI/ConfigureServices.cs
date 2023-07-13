using Application.Common.Interfaces;
using Domain.Repositories;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
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
            services.AddScoped<ILanguageRepository, LanguageRepository>(); 

            // Other dependencies specific to the presentation layer
            services.AddHttpContextAccessor();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
                options.SuppressModelStateInvalidFilter = true);

            services.AddControllers();

            return services;
        }

    }
}
