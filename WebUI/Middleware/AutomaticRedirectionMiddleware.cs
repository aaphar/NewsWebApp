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

            // Check if the current path is the root URL without the language code
            if (requestPath == "/blog")
            {
                // Redirect to the English version of the root URL
                context.Response.Redirect("/en/blog");
                return;
            }

            await _next(context);
        }
    }
}
