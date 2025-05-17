using FundaAssignment.DTOs;
using FundaAssignment.Enums;
using MediatR;

namespace FundaAssignment.Queries;

public class BrokersQuery(string searchQuery, FundaOfferTypes type)
    : IRequest<IEnumerable<BrokerDto>>
{
    public string SearchQuery { get; } = searchQuery;
    public FundaOfferTypes Type { get; } = type;
}