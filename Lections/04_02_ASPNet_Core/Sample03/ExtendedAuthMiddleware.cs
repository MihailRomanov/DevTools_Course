namespace Sample
{
    public class ExtendedAuthMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string passwd;

        public ExtendedAuthMiddleware(RequestDelegate next, string passwd)
        {
            this.next = next;
            this.passwd = passwd;
        }

        public async Task Invoke(HttpContext context) 
        {
            if (context.Request.Query["pass"] == passwd)
            {
                await next(context);
            }
            else
            { 
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
            }
        }
    }

    public static class ExtendedAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UsePasswordAuth(this IApplicationBuilder applicationBuilder, string passwd)
        {
            return applicationBuilder.UseMiddleware<ExtendedAuthMiddleware>(passwd);
        }
    }
}
