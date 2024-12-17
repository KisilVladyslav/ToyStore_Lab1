using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        // Логування вхідного запиту
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

        // Пропускаємо запит до наступного middleware
        await _next(context);

        stopwatch.Stop();

        // Логування часу обробки запиту
        Console.WriteLine($"Request processed in {stopwatch.ElapsedMilliseconds} ms");
    }
}
