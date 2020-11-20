using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class Preference
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}