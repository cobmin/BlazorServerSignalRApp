using System.Text.Json.Serialization;

namespace BlazorServerSignalRApp.Loopring.Models
{
    public class ResultInfo
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }

    public class EnsResult
    {
        [JsonPropertyName("resultInfo")]
        public ResultInfo? ResultInfo { get; set; }

        [JsonPropertyName("data")]
        public string? Data { get; set; }
    }
}
