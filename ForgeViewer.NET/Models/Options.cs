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
        public string Env { get; init; }
        [JsonPropertyName("api")]
        public string Api { get; init; }
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; init; }
        [JsonPropertyName("language")]
        public string Language { get; init; }

        [JsonIgnore]
        public Func<Task<string>>? GetAccessToken { get; init; }

        [JsonPropertyName("getAccessToken")]
        public bool IsGetAccessToken => GetAccessToken is not null;
    }
}