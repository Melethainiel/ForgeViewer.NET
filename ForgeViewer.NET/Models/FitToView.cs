using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class FitToView
    {
        [JsonPropertyName("nodeIdArray")]
        public IEnumerable<double> NodeIdArray { get; set; }
        [JsonPropertyName("immediate")]
        public bool Immediate { get; set; }
    }
}