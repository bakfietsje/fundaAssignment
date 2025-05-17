using System.Text.Json.Serialization;

namespace FundaAssignment.DTOs.FundaDtos;

public record FundaApiResponse
{
    [JsonPropertyName("Objects")]
    public List<FundaObject> Objects { get; init; } = [];
    
    [JsonPropertyName("Paging")]
    public PagingInfo Paging { get; init;  }
}