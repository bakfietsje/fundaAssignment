using FundaAssignment.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FundaAssignment.Filters;

public class BrokersFilters
{
    [BindRequired]
    public string SearchQuery { get; set; }

    [BindRequired]
    public FundaOfferTypes Type { get; set; }
}