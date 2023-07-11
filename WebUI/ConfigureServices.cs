using Infrastructure;
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
