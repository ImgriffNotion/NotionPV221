namespace NotionBack.Middleware.Auth
{
    public static class AuthMiddlewareExtention
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AuthMiddleware>();
        }
    }
}
