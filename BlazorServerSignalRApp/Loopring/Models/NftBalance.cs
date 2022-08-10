using System.Text.Json.Serialization;

namespace BlazorServerSignalRApp.Loopring.Models
{
    public class NftBalanceData
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("accountId")]
        public int AccountId { get; set; }

        [JsonPropertyName("tokenId")]
        public int TokenId { get; set; }

        [JsonPropertyName("nftData")]
        public string? NftData { get; set; }

        [JsonPropertyName("tokenAddress")]
        public string? TokenAddress { get; set; }

        [JsonPropertyName("nftId")]
        public string? NftId { get; set; }

        [JsonPropertyName("nftType")]
        public string? NftType { get; set; }

        [JsonPropertyName("total")]
        public string? Total { get; set; }

        [JsonPropertyName("locked")]
        public string? Locked { get; set; }

        [JsonPropertyName("pending")]
        public Pending? Pending { get; set; }

        [JsonPropertyName("deploymentStatus")]
        public string? DeploymentStatus { get; set; }

        [JsonPropertyName("isCounterFactualNFT")]
        public bool IsCounterFactualNFT { get; set; }
    }

    public class Pending
    {
        [JsonPropertyName("withdraw")]
        public string? Withdraw { get; set; }

        [JsonPropertyName("deposit")]
        public string? Deposit { get; set; }
    }

    public class NftBalance
    {
        [JsonPropertyName("totalNum")]
        public int TotalNum { get; set; }

        [JsonPropertyName("data")]
        public List<NftBalanceData>? Data { get; set; }
    }
}
