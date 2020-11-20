using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class ViewerRestored
    {
        [JsonPropertyName("value")]
        public bool Value { get; set; }
    }
}