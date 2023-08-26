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
                options.Conventions.AddPageRoute("/Single", "{title}");
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

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "languagePage",
            //        pattern: "{language}/{controller}/{pageIndex:int}",
            //        defaults: new { action = "Index" }); // Adjust the action as needed
            //});


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<AdminAuthenticationMiddleware>();

            app.MapControllers();

            app.MapRazorPages();

            app.Run();
        }
    }
}