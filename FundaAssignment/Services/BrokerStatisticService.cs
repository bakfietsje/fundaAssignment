using FundaAssignment.Clients;
using FundaAssignment.Enums;
using FundaAssignment.Models;

namespace FundaAssignment.Services;

public class BrokerStatisticService(IFundaApiClient fundaApiClient) : IBrokerStatisticService
{
    public async Task<IEnumerable<Broker>> GetBrokerStatisticsAsync(string searchPath, FundaOfferTypes type,CancellationToken cancellationToken)
    {
        var listings = await fundaApiClient.GetListingsAsync(searchPath, type, cancellationToken);

        var topBrokers = listings
            .GroupBy(x => new { x.Id, x.Name })
            .Select(g => new Broker(g.Key.Id, g.Key.Name)
            {
                Count = g.Count(),
            })
            .OrderByDescending(x => x.Count)
            .Take(10)
            .ToList();

        return topBrokers;
    }
}