using NotionBack.Middleware.Auth;

namespace NotionBack.Middleware.PageTypes
{
    public static class PageTypeCreatorMiddlewareExtention
    {
        public static IApplicationBuilder UsePageTypeCreator(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PageTypeCreatoreMiddleware>();
        }
    }
}
