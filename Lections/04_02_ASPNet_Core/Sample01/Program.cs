var builder = WebApplication.CreateBuilder();
builder.WebHost.UseUrls("http://localhost:4000");

var app = builder.Build();

app.Run(context
   => context.Response.WriteAsync("Hello, World!"));

app.Run();
