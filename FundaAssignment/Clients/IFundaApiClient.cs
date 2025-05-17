using FundaAssignment.Enums;
using FundaAssignment.Models;

namespace FundaAssignment.Clients;

public interface IFundaApiClient
{
    Task<IEnumerable<Broker>> GetListingsAsync(string searchPath, FundaOfferTypes type, CancellationToken cancellationToken);
}