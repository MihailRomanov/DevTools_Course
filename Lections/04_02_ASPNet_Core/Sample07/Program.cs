using AuthLib;
using Humanizer;
using Sample04;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddSingleton<IFailCountStore, InMemoryFailCountStore>();
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        app.UsePasswordAuth("4567");
        app.UseRouting();
        app.MapDefaultControllerRoute();

        app.Run();
    }
}