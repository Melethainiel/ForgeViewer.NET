using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class Tool
    {
        [JsonPropertyName("toolName")]
        public string ToolName { get; set; }
        [JsonPropertyName("active")]
        public bool Active { get; set; }
    }
}