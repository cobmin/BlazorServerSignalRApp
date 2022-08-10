using System.Text.Json.Serialization;

namespace BlazorServerSignalRApp.Loopring.Models
{
    public class StorageId
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("offchainId")]
        public int OffchainId { get; set; }
    }
}
