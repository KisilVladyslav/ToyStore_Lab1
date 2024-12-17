using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToyStoreDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IToyService, ToyService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ICartService, CartService>();

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<RequestMethodMiddleware>();

app.MapGet("/toy", async context =>
{
    // Обробка GET запиту (отримання списку іграшок або чогось іншого)
    Console.WriteLine("Handling GET /toy");
    await context.Response.WriteAsync("GET request processed.");
});

// Обробка HTTP POST запитів на /toy
app.MapPost("/toy", async context =>
{
    // Обробка POST запиту (створення нової іграшки)
    Console.WriteLine("Handling POST /toy");

    // Отримуємо дані з тіла запиту
    var toyName = await new System.IO.StreamReader(context.Request.Body).ReadToEndAsync();
    await context.Response.WriteAsync($"POST request processed. Toy Name: {toyName}");
});

// Використовуємо стандартні middleware для статичних файлів та маршрутизації
app.UseStaticFiles();


app.Run();
