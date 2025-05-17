using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace FundaAssignment.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum FundaOfferTypes
{
    koop = 0
}