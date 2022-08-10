using System.Text.Json.Serialization;

namespace BlazorServerSignalRApp.Loopring.Models
{
    public class Fee
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("fee")]
        public string? FFee { get; set; }

        [JsonPropertyName("discount")]
        public int Discount { get; set; }
    }

    public class OffchainFee
    {
        [JsonPropertyName("gasPrice")]
        public string? GasPrice { get; set; }

        [JsonPropertyName("fees")]
        public List<Fee>? Fees { get; set; }
    }
}
