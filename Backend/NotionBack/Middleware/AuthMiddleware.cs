namespace NotionBack.Middleware
{
    public class AuthMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext httpContext)
        {


           await this._next.Invoke(httpContext);
        }
    }
}
