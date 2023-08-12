using Application.CommandQueries.Language.Commands.CreateLanguage;
using Application.Common.Behaviours;
using Application.Operations.Categories.Commands.CreateCategory;
using Application.Operations.CategoryTranslations.Commands.CreateCategoryTranslation;
using Application.Operations.Posts.Commands.CreatePost;
using Application.Operations.PostTranslations.Commands.CreatePostTranslation;
using Application.Operations.Roles.Commands.CreateRole;
using Application.Operations.Users.Commands.CreateUser;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
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

            // command validators
            services.AddScoped<IValidator<CreateLanguageCommand>, CreateLanguageCommandValidator>();
            services.AddScoped<IValidator<CreateRoleCommand>, CreateRoleCommandValidator>();
            services.AddScoped<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
            services.AddScoped<IValidator<CreateCategoryTranslationCommand>, CreateCategoryTranslationCommandValidator>();
            services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
            services.AddScoped<IValidator<CreatePostTranslationCommand>, CreatePostTranslationCommandValidator>();
            services.AddScoped<IValidator<CreatePostCommand>, CreatePostCommandValidator>();



            return services;
        }
    }
}
