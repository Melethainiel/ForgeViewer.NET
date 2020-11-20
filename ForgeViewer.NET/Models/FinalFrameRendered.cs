using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class FinalFrameRendered
    {
        [JsonPropertyName("finalFrame")]
        public bool FinalFrame { get; set; }
    }
}