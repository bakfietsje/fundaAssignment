using System.Net.Http.Json;
using FundaAssignment.DTOs;

namespace FundaAssignment.Commands;

public class TopBrokersWithGardenCommand(IHttpClientFactory httpClientFactory) : ICommand
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Funda");
    
    public string Name => "2. Top 10 brokers in Amsterdam that offer a garden";
    
    public async Task ExecuteAsync()
    {
        var brokers = await _httpClient.GetFromJsonAsync<List<BrokerDto>>("api/brokers?zo=/amsterdam/tuin&type=koop&page=1&pageSize=25");

        Console.WriteLine("Now displaying top 10 brokers in Amsterdam that offer a garden");
        foreach (var broker in brokers ?? Enumerable.Empty<BrokerDto>())
        {
            Console.WriteLine($"{broker.Name,-30} amount of objects:  {broker.Count,3}");
        }
    }
}