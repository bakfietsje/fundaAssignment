using FundaAssignment.Enums;
using FundaAssignment.Models;

namespace FundaAssignment.Services;

public interface IBrokerStatisticService
{
    Task<IEnumerable<Broker>> GetBrokerStatisticsAsync(string searchPath, FundaOfferTypes type, CancellationToken cancellationToken);
}