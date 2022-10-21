using Microsoft.AspNetCore.Builder;

namespace AuthLib
{
    public static class ExtendedAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UsePasswordAuth(this IApplicationBuilder applicationBuilder, string passwd)
        {
            return applicationBuilder.UseMiddleware<ExtendedAuthMiddleware>(passwd);
        }
    }
}
