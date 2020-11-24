using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class PropertyResult 
    {
        [JsonPropertyName("dbId")]
        public double DbId { get; set; }
        [JsonPropertyName("externalId")]
        public string ExternalId { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("properties")]
        public IEnumerable<Property>? Properties { get; set; }
    }
}