using System.Text.Json.Serialization;

namespace FundaAssignment.DTOs.FundaDtos;

public class PagingInfo
{
    [JsonPropertyName("AantalPaginas")]
    public int TotalPages { get; set; }

    [JsonPropertyName("HuidigePagina")]
    public int CurrentPage { get; set; }

    [JsonPropertyName("VolgendeUrl")]
    public string? NextUrl { get; set; }

    [JsonPropertyName("VorigeUrl")]
    public string? PreviousUrl { get; set; }
}