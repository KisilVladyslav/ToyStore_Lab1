using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class RequestMethodMiddleware
{
    private readonly RequestDelegate _next;

    public RequestMethodMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Get)
        {
            // Спеціальна обробка для GET запитів
            Console.WriteLine("Processing GET request...");
        }
        else if (context.Request.Method == HttpMethods.Post)
        {
            // Спеціальна обробка для POST запитів
            Console.WriteLine("Processing POST request...");
        }

        await _next(context);  // Пропускаємо запит до наступного middleware
    }
}
