using BlazorServerSignalRApp.Loopring.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RestSharp;

namespace BlazorServerSignalRApp.Loopring
{
    public class LoopringService : Hub, ILoopringService, IDisposable
    {
        const string _baseUrl = "https://api3.loopring.io";

        readonly RestClient _client;

        public LoopringService()
        {
            _client = new RestClient(_baseUrl);
        }

        public async Task<OffchainFee> GetOffChainFee(string apiKey, int accountId, int requestType, string tokenAddress)
        {
            var request = new RestRequest("api/v3/user/nft/offchainFee");
            request.AddHeader("x-api-key", apiKey);
            request.AddParameter("accountId", accountId);
            request.AddParameter("requestType", requestType);
            request.AddParameter("tokenAddress", tokenAddress);
            try
            {
                var response = await _client.GetAsync(request);
                var data = JsonConvert.DeserializeObject<OffchainFee>(response.Content!);
                return data;
            }
            catch (HttpRequestException httpException)
            {
                Console.WriteLine($"Error getting off chain fee: {httpException.Message}");
                return null;
            }
        }

        public async Task<StorageId> GetNextStorageId(string apiKey, int accountId, int sellTokenId)
        {
            var request = new RestRequest("api/v3/storageId");
            request.AddHeader("x-api-key", apiKey);
            request.AddParameter("accountId", accountId);
            request.AddParameter("sellTokenId", sellTokenId);
            try
            {
                var response = await _client.GetAsync(request);
                var data = JsonConvert.DeserializeObject<StorageId>(response.Content!);
                return data;
            }
            catch (HttpRequestException httpException)
            {
                Console.WriteLine($"Error getting storage id: {httpException.Message}");
                return null;
            }
        }

        public async Task<OffchainFee> GetOffChainFeeNftTransfer(string apiKey, int accountId, int requestType, string amount)
        {
            var request = new RestRequest("api/v3/user/nft/offchainFee");
            request.AddHeader("x-api-key", apiKey);
            request.AddParameter("accountId", accountId);
            request.AddParameter("requestType", requestType);
            request.AddParameter("amount", amount);
            try
            {
                var response = await _client.GetAsync(request);
                var data = JsonConvert.DeserializeObject<OffchainFee>(response.Content!);
                return data;
            }
            catch (HttpRequestException httpException)
            {
                Console.WriteLine($"Error getting off chain fee: {httpException.Message}");
                return null;
            }
        }

        public async Task<string> SubmitNftTransfer(string apiKey, string exchange, int fromAccountId, string fromAddress, int toAccountId, string toAddress, int nftTokenId, string nftAmount, int maxFeeTokenId, string maxFeeAmount, int storageId, long validUntil, string eddsaSignature, string ecdsaSignature, string nftData)
        {
            var request = new RestRequest("api/v3/nft/transfer");
            request.AddHeader("x-api-key", apiKey);
            request.AddHeader("x-api-sig", ecdsaSignature);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("exchange", exchange);
            request.AddParameter("fromAccountId", fromAccountId);
            request.AddParameter("fromAddress", fromAddress);
            request.AddParameter("toAccountId", toAccountId);
            request.AddParameter("toAddress", toAddress);
            request.AddParameter("token.tokenId", nftTokenId);
            request.AddParameter("token.amount", nftAmount);
            request.AddParameter("token.nftData", nftData);
            request.AddParameter("maxFee.tokenId", maxFeeTokenId);
            request.AddParameter("maxFee.amount", maxFeeAmount);
            request.AddParameter("storageId", storageId);
            request.AddParameter("validUntil", validUntil);
            request.AddParameter("eddsaSignature", eddsaSignature);
            request.AddParameter("ecdsaSignature", ecdsaSignature);
            try
            {
                var response = await _client.ExecutePostAsync(request);
                var data = response.Content;
                return data;
            }
            catch (HttpRequestException httpException)
            {
                Console.WriteLine($"Error submitting nft transfer: {httpException.Message}");
                return null;
            }
        }

        public async Task<NftBalance> GetNftBalance(string apiKey, int accountId, string nftData)
        {
            var request = new RestRequest("api/v3/user/nft/balances");
            request.AddHeader("x-api-key", apiKey);
            request.AddParameter("accountId", accountId);
            request.AddParameter("nftDatas", nftData);
            try
            {
                var response = await _client.GetAsync(request);
                var data = JsonConvert.DeserializeObject<NftBalance>(response.Content!);
                return data;
            }
            catch (HttpRequestException httpException)
            {
                Console.WriteLine($"Error getting nft balance: {httpException.Message}");
                return null;
            }
        }

        public async Task GetEnsorHex(string userInput)
        {
            if (userInput == null || (!userInput.ToLower().Trim().EndsWith(".eth") && !userInput.ToLower().Trim().StartsWith("0x")))
            {
                userInput = "Enter hex address or Ens.";
                await Clients.All.SendAsync("ReceiveMessage", userInput);
            }
            else if (!userInput.ToLower().Trim().EndsWith(".eth"))
            {
                var request = new RestRequest("api/wallet/v3/resolveName");
                request.AddParameter("owner", userInput.ToLower());
                try
                {
                    var response = await _client.GetAsync(request);
                    var data = JsonConvert.DeserializeObject<EnsResult>(response.Content!);
                    if (data.Data == "")
                    {
                        data.Data = "This is not a valid wallet address";
                    }
                    await Clients.All.SendAsync("ReceiveMessage", data.Data);
                }
                catch (HttpRequestException httpException)
                {
                    Console.WriteLine($"Error getting ens: {httpException.Message}");
                    await Clients.All.SendAsync("ReceiveMessage", userInput);
                }
            }
            else
            {
                var request = new RestRequest("api/wallet/v3/resolveEns");
                request.AddParameter("fullName", userInput.ToLower());
                try
                {
                    var response = await _client.GetAsync(request);
                    var data = JsonConvert.DeserializeObject<EnsResult>(response.Content!);
                    if (data.Data == null)
                    {
                        data.Data = "This Hex doesn't have an Ens.";
                    }
                    await Clients.All.SendAsync("ReceiveMessage", data.Data);
                }
                catch (HttpRequestException httpException)
                {
                    Console.WriteLine($"Error getting ens: {httpException.Message}");
                    await Clients.All.SendAsync("ReceiveMessage", userInput);
                }
            }
            
        }
        public new void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
