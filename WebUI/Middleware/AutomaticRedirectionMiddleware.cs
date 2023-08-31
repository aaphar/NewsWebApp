using System.Text.RegularExpressions;

namespace WebUI.Middleware
{
    public class AutomaticRedirectionMiddleware
    {
        private readonly RequestDelegate _next;

        // Add a list of paths to exclude from redirection
        private readonly List<string> _excludedPaths = new List<string>
        {
            "/admin",
            "/Home",
            "/upload",
            "/images",
            "/css",
            "/js",
            "/ckeditor5",
            "/ckeditor/ckeditor.js",
            "/assets",
            "/vendor",
            // Add other paths as needed
        };

        public AutomaticRedirectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestPath = context.Request.Path;

            // Check if the current path is in the excluded list
            if (_excludedPaths.Any(path => requestPath.StartsWithSegments(path)))
            {
                await _next(context);
                return;
            }

            // Check if the current path is not already containing a language code
            if (!Regex.IsMatch(requestPath, @"^/\w{2}/"))
            {
                // Redirect to the same path with a default language code (e.g., "en")
                context.Response.Redirect($"/en{requestPath}");
                return;
            }

            await _next(context);
        }
    }
}
