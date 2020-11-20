using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class Explode
    {
        [JsonPropertyName("scale")]
        public double Scale { get; set; }
    }
}