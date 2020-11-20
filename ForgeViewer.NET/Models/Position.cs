using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class Position
    {
        [JsonPropertyName("top")]
        public double Top { get; set; }
        [JsonPropertyName("left")]
        public double Left { get; set; }
    }
}