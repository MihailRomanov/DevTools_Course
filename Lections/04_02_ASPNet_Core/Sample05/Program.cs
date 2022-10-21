using AuthLib;
using Humanizer;
using Sample04;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddSingleton<IFailCountStore, InMemoryFailCountStore>();

        var app = builder.Build();

        app.UsePasswordAuth("4567");

        app.MapGet("/hello", (string? name) => GetHelloByeString("Hello", name));
        app.MapGet("/bye", (string? name) => GetHelloByeString("Bye", name));
        app.MapGet("/", () => "What???");

        //app.MapGet("/{word}", (string word, string? name) => GetHelloByeString(word, name));
        //app.MapGet("/{word:regex(^hello|bye$)}", (string word, string? name) => GetHelloByeString(word, name));

        app.Run();
    }

    private static string GetHelloByeString(string helloBye, string? name)
    {
        name = string.IsNullOrEmpty(name) ? "World" : name;
        return $"{helloBye.Transform(To.TitleCase)}, {name}\n";
    }
}