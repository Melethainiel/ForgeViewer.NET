using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class Objects
    {
        [JsonPropertyName("nodeIdArray")]
        public IEnumerable<double> NodeIdArray { get; set; }
    }
}