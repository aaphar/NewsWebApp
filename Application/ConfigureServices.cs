using Application.CommandQueries.Language.Commands.CreateLanguage;
using Application.Common.Behaviours;
using Application.Operations.Categories.Commands.CreateCategory;
using Application.Operations.CategoryTranslations.Commands.CreateCategoryTranslation;
using Application.Operations.Roles.Commands.CreateRole;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            // command validators
            services.AddScoped<IValidator<CreateLanguageCommand>, CreateLanguageCommandValidator>();
            services.AddScoped<IValidator<CreateRoleCommand>, CreateRoleCommandValidator>();
            services.AddScoped<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
            services.AddScoped<IValidator<CreateCategoryTranslationCommand>, CreateCategoryTranslationCommandValidator>();

            return services;
        }
    }
}
