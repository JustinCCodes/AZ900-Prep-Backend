namespace AZ900Prep.Api.Infrastructure;

// Extension methods for configuring middleware
public static class MiddlewareExtensions
{
    // Adds security headers to HTTP responses
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            // Mitigates Clickjacking
            context.Response.Headers.Append("X-Frame-Options", "DENY");
            // Mitigates XSS
            context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            // Referrer Policy
            context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");

            await next();
        });
    }
}
