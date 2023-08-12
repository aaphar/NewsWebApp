using Microsoft.AspNetCore.Authorization;

namespace WebUI.Middleware;

public class AdminAuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AdminAuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint != null && RequiresAuthorization(endpoint))
        {
            if (!context.User.Identity.IsAuthenticated)
            {
                context.Response.Redirect("admin/index");
                return;
            }
            else
            {
                await _next(context);
                return;
            }
        }
        await _next(context);
    }

    private bool RequiresAuthorization(Endpoint endpoint)
    {
        return endpoint.Metadata.GetMetadata<IAuthorizeData>() != null;
    }
}
