using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class ExtensionResponse
    {
        [JsonPropertyName("extensionId")]
        public string ExtensionId { get; set; }
    }
}