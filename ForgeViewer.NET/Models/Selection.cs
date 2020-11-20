using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ForgeViewer.NET.Models
{
    public class Selection
    {
        [JsonPropertyName("fragIdsArray")]
        public IEnumerable<double>? FragIdsArray { get; set; }
        [JsonPropertyName("dbIdArray")]
        public IEnumerable<double>? DbIdArray { get; set; }
        [JsonPropertyName("nodeArray")]
        public IEnumerable<double>? NodeArray { get; set; }
    }
}