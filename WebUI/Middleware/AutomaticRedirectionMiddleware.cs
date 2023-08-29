using System.Text.RegularExpressions;

namespace WebUI.Middleware
{
    public class AutomaticRedirectionMiddleware
    {
        private readonly RequestDelegate _next;

        public AutomaticRedirectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestPath = context.Request.Path;

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
