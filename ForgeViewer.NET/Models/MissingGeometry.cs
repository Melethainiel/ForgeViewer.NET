using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class MissingGeometry
    {
        [JsonPropertyName("delay")]
        public bool Delay { get; set; }
    }
}