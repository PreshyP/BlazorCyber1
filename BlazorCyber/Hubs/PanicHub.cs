using Microsoft.AspNetCore.SignalR;

namespace BlazorCyber.Hubs
{
    public class PanicHub:Hub
    {
        public async Task SendPanicAlert(string message)
        {
            await Clients.All.SendAsync("ReceivePanicAlert", message);
        }
    }
}

