using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace ForgeViewer.NET.Models
{
    public class Options
    {
        public Options()
        {
            Env = "AutodeskProduction";
            Api = "derivativeV2";
            AccessToken = "";
            Language = "fr";
        }
        
        [JsonPropertyName("env")]
        public string Env { get; set; }
        [JsonPropertyName("api")]
        public string Api { get; set; }
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonIgnore]
        public Func<Task<string>>? GetAccessToken { get; set; }

        [JsonPropertyName("getAccessToken")]
        public bool IsGetAccessToken => GetAccessToken is not null;
    }
}