using System.Text.Json.Serialization;

namespace FundaAssignment.DTOs.FundaDtos;

public record FundaObject
{
    [JsonPropertyName("MakelaarId")]
    public int BrokerId { get; init; }
    
    [JsonPropertyName("MakelaarNaam")]
    public string BrokerName { get; init; }
}