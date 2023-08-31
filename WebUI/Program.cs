using Infrastructure;
using Application;
using WebUI.Middleware;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddRazorPages(options =>
            {               
				options.Conventions.Add(new CulturePageRouteModelConvention());
            });


            //builder.Services.AddRazorPages(options =>
            //{
            //    options.Conventions.AddPageRoute("/Single", "{title}");
            //});

            builder.Services.AddSession(options =>
            {
                // Configure session options here if needed
            });

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseMiddleware<AutomaticRedirectionMiddleware>(); // Your existing middleware
            app.UseMiddleware<AdminAuthenticationMiddleware>(); // Your existing middleware

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

           

            app.MapControllers();

            app.MapRazorPages();

            app.UseSession();
            app.Run();
        }
    }
}