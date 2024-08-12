using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class NotifyHub : Hub
{
    public async Task SendMessageToClient(string connectionId, string message)
    {
        await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
    }

    public async Task SendMessageToGroup(string groupName, string message)
    {
        await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
    }

    public override async Task OnConnectedAsync()
    {
        // Send welcome message or perform initial setup
        await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessage", "Server", "Welcome to the chat!");

        await base.OnConnectedAsync();
    }
}
