using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorCyber.Services
{
    public class MySignalRService : IAsyncDisposable
    {
        private HubConnection _hubConnection;
        private string _connectionId;

        public MySignalRService()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:7125/notifyHub")
                .Build();

            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Console.WriteLine($"Received message from {user}: {message}");
                // Handle incoming messages here
            });

            ConnectToHub();
        }

        public async Task ConnectToHub()
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                try
                {
                    await _hubConnection.StartAsync();
                    _connectionId = _hubConnection.ConnectionId;
                    Console.WriteLine($"SignalR connected. Connection ID: {_connectionId}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to SignalR hub: {ex.Message}");
                }
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (_hubConnection != null)
            {
                await _hubConnection.StopAsync();
                await _hubConnection.DisposeAsync();
                Console.WriteLine("SignalR connection stopped and disposed.");
            }
        }

        public async Task SendMessageAsync(string user, string message)
        {
            try
            {
                await ConnectToHub();
                if (_hubConnection.State == HubConnectionState.Connected)
                {
                    await _hubConnection.SendAsync("SendMessage", user, message);
                    Console.WriteLine($"Sent message to server: {message}");
                }
                else
                {
                    Console.WriteLine("SignalR connection is not established. Cannot send message.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message via SignalR: {ex.Message}");
            }
        }
    }
}
