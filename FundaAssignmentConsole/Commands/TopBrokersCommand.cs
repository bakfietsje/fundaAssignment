using System.Net.Http.Json;
using FundaAssignment.DTOs;

namespace FundaAssignment.Commands;

public class TopBrokersCommand(IHttpClientFactory httpClientFactory) : ICommand
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("Funda");

    public string Name => "1. Top 10 brokers in Amsterdam";
    
    private readonly string url = $"api/brokers?searchQuery={Uri.EscapeDataString("/amsterdam")}&type=koop";

    public async Task ExecuteAsync()
    {
        var brokers = await _httpClient.GetFromJsonAsync<List<BrokerDto>>(url);

        Console.WriteLine("Now displaying top 10 brokers in Amsterdam");
        foreach (var broker in brokers ?? Enumerable.Empty<BrokerDto>())
        {
            Console.WriteLine($"{broker.Name,-30} amount of objects: {broker.Count,3}");
        }
    }
}