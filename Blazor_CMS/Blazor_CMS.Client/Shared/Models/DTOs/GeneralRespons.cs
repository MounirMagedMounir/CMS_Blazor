using System.Text.Json.Serialization;

namespace Blazor_CMS.Client.Shared.Models.DTOs
{
    public class GeneralRespons
    {
        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }
    }
}
