using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class Dimension
    {
        [JsonPropertyName("width")]
        public double Width { get; set; }
        [JsonPropertyName("height")]
        public double Height { get; set; }
    }
}