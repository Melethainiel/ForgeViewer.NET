using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class ProgressUpdate
    {
        [JsonPropertyName("percent")]
        public double Percent { get; set; }
        [JsonPropertyName("state")]
        public double State { get; set; }
    }
}