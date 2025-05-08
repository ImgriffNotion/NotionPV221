namespace NotionBack.Middleware.Token
{
    public class UpdateTokenMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        public async Task Invoke(HttpContext httpContext)
        {

        }
    }
}
