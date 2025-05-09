namespace NotionBack.Middleware.Token
{
    public static class UpdateTokenMiddlewareExtention
    {
        public static IApplicationBuilder UseUpdateTokenMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UpdateTokenMiddleware>();
        }
    }
}
