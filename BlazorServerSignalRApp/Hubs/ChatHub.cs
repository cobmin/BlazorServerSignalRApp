using Microsoft.AspNetCore.SignalR;

namespace BlazorServerSignalRApp.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            user = $"{user} changed";
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}