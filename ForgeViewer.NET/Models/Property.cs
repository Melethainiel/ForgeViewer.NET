using System.Text.Json.Serialization;
using ForgeViewer.NET.Misc;

namespace ForgeViewer.NET.Models
{
    public class Property {
        [JsonPropertyName("attributeName")]
        public string AttributeName { get; set; }
        [JsonPropertyName("displayCategory")]
        public string DisplayCategory { get; set; }
        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }
        [JsonPropertyName("displayValue")]
        [JsonConverter(typeof(JsonObjConverter))]
        public object DisplayValue { get; set; }
        [JsonPropertyName("hidden")]
        [JsonConverter(typeof(JsonBoolConverter))]
        public bool Hidden { get; set; }
        [JsonPropertyName("type")]
        public double Type { get; set; }
        [JsonPropertyName("units")]
        public string Units { get; set; }
    }
}