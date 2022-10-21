using Microsoft.AspNetCore.Http;

namespace AuthLib
{
    public class ExtendedAuthMiddleware
    {
        private readonly RequestDelegate next;
        private readonly string passwd;
        private const int MaxFailCount = 5;

        public ExtendedAuthMiddleware(RequestDelegate next, string passwd)
        {
            this.next = next;
            this.passwd = passwd;
        }

        public async Task Invoke(HttpContext context, IFailCountStore failCountStore)
        {
            if ((context.Request.Path == "/favicon.ico") ||
                (context.Request.Query["pass"] == passwd && failCountStore.Fails < MaxFailCount))
            {
                await next(context);
            }
            else
            {
                failCountStore.Fails++;
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync($"Unauthorized. Tries: {failCountStore.Fails}");
            }
        }
    }
}
