using NotionBack.Models;

namespace NotionBack.Middleware.SlugNavigate
{
    public class SlugNavigateMiddleware(RequestDelegate next, ILogger<SlugNavigateMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<SlugNavigateMiddleware> _logger = logger;


        public async Task Invoke(HttpContext httpContext, IServiceProvider serviceProvider, AppUserContext userContext)
        {
            var path = httpContext.Request.Path.Value?.Trim('/');

            if (!string.IsNullOrEmpty(path) && !path.StartsWith("imgriff") && !path.StartsWith("api"))
            {
                // Rewrite the path to your controller's expected route
                httpContext.Request.Path = "/imgriff/pages";
                httpContext.Request.QueryString = new QueryString($"?slug={path}");
            }

            await next(httpContext);
        }
    }
}
