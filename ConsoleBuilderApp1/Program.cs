
/*
 * Added by R#
 */
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleBuilderApp1;
using Microsoft.Extensions.Logging;
using Pri.ConsoleApplicationBuilder;

partial class Program(ILogger<Program> logger, HttpClient httpClient)
{
    static async Task Main(string[] args)
    {
        var builder = ConsoleApplication.CreateBuilder(args);
        builder.Services.AddHttpClient<Program>(httpClient =>
        {
            httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        });
        var program = builder.Build<Program>();
        await program.Run();
        Console.ReadLine();
    }

    private async Task Run()
    {
        logger.LogInformation("Hello, World!");
        logger.LogInformation(await httpClient.GetStringAsync("todos/1"));
    }
}