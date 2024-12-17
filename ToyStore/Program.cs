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
    // ������� GET ������ (��������� ������ ������� ��� ������ ������)
    Console.WriteLine("Handling GET /toy");
    await context.Response.WriteAsync("GET request processed.");
});

// ������� HTTP POST ������ �� /toy
app.MapPost("/toy", async context =>
{
    // ������� POST ������ (��������� ���� �������)
    Console.WriteLine("Handling POST /toy");

    // �������� ��� � ��� ������
    var toyName = await new System.IO.StreamReader(context.Request.Body).ReadToEndAsync();
    await context.Response.WriteAsync($"POST request processed. Toy Name: {toyName}");
});

// ������������� ��������� middleware ��� ��������� ����� �� �������������
app.UseStaticFiles();


app.Run();
