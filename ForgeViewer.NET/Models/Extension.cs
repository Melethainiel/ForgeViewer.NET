using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class Extension
    {
        [JsonPropertyName("extensionId")]
        public string ExtensionId { get; set; }
    }
}