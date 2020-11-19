using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ForgeViewer.NET.Models
{
    public class Options
    {
        private Func<Task<string>>? _getAccessToken;
        private string? _getAccessTokenId;

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
        public Func<Task<string>>? GetAccessToken
        {
            get => _getAccessToken;
            set
            {
                _getAccessToken = value;
                GetAccessTokenId = Guid.NewGuid().ToString();
            }
        }

        [JsonPropertyName("getAccessToken")]
        public string GetAccessTokenId
        {
            get => _getAccessTokenId ?? throw new Exception();
            private set => _getAccessTokenId = value;
        }
    }
}