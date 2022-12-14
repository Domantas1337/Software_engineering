using System.Globalization;
public class LogRequestsMiddleware
{
    private readonly RequestDelegate _next;

    public LogRequestsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("Request started");

        var timeOfRequest = DateTime.Now;
        var requestPath = context.Request.Path;
        var requestMethod = context.Request.Method;
        var requestHeaders = context.Request.Headers.ToString();

        await _next(context);

    }
}

public static class LogRequestsMiddlewareExtensions
{
    public static IApplicationBuilder UseLogRequests(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LogRequestsMiddleware>();
    }
}
