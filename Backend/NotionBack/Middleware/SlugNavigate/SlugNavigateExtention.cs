using NotionBack.Middleware.Auth;

namespace NotionBack.Middleware.SlugNavigate
{
    public static class SlugNavigateExtention
    {
        public static IApplicationBuilder UseSlugNavigate(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SlugNavigateMiddleware>();
        }
    }
}
