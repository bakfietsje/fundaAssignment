using AutoMapper;
using FundaAssignment.DTOs;
using FundaAssignment.Queries;
using FundaAssignment.Services;
using MediatR;

namespace FundaAssignment.QueryHandlers;

public class BrokersQueryHandler(IBrokerStatisticService _brokerStatisticService, IMapper mapper) : IRequestHandler<BrokersQuery, IEnumerable<BrokerDto>>
{
    public async Task<IEnumerable<BrokerDto>> Handle(BrokersQuery request, CancellationToken cancellationToken)
    {
        var brokers = await _brokerStatisticService.GetBrokerStatisticsAsync(
            request.SearchQuery,
            request.Type,
            cancellationToken);

        var result = mapper.Map<IEnumerable<BrokerDto>>(brokers);
        
        return result;
    }
}