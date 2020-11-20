using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class NavigationMode
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }
}