using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using ToyStore.Services.Interfaces;
using ToyStore.Services;
using System.Text.Json;

public class RequestMethodMiddleware
{
    private readonly RequestDelegate _next;

    public RequestMethodMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IToyService toyService)
    {
        if (context.Request.Method == HttpMethods.Get && context.Request.Path.StartsWithSegments("/toys"))
        {
            await GetAllToysAsync(context, toyService);
            //Console.WriteLine("Processing GET request...");
        }
        else if (context.Request.Method == HttpMethods.Post)
        {
            Console.WriteLine("Processing POST request...");
        }

        await _next(context);  // Пропускаємо запит до наступного middleware
    }

    private async Task GetAllToysAsync(HttpContext context, IToyService toyService)
    {
        var toys = await toyService.GetAllToysAsync();
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonSerializer.Serialize(toys));
    }
}
