using FundaAssignment.Commands;
using FundaAssignment.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FundaAssignment;

class Program
{
    private static async Task Main()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("appsettings.local.json", optional: false);
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<FundaAssignmentOptions>(context.Configuration.GetSection("Funda"));
                
                services.AddHttpClient("Funda", (sp, client) =>
                {
                    var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<FundaAssignmentOptions>>().Value;
                    client.BaseAddress = new Uri($"{options.BaseUrl}:{options.Port}");
                });
                
                services.AddScoped<TopBrokersCommand>();
                services.AddScoped<TopBrokersWithGardenCommand>();
            })
            .Build();

        using var scope = host.Services.CreateScope();

        var commands = new List<ICommand>
        {
            scope.ServiceProvider.GetRequiredService<TopBrokersCommand>(),
            scope.ServiceProvider.GetRequiredService<TopBrokersWithGardenCommand>()
        };

        while (true)
        {
            Console.WriteLine("Choose an option:");
            foreach (var cmd in commands)
            {
                Console.WriteLine(cmd.Name);
            }
            Console.WriteLine("3. Exit");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await commands[0].ExecuteAsync();
                    break;
                case "2":
                    await commands[1].ExecuteAsync();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }

            Console.WriteLine("\nPress a key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}