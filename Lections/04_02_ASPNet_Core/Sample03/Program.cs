using Sample;

var builder = WebApplication.CreateBuilder();
var app = builder.Build();

app.UseMiddleware<AuthMiddleware>();
//app.UsePasswordAuth("4567");

app.Use(async (context, next) =>
{
    await next.Invoke();

    var name = context.Request.Query["name"];
    name = string.IsNullOrEmpty(name) ? "World" : name;

    await context.Response.WriteAsync($"Hello, {name}\n");
});

app.Run();
