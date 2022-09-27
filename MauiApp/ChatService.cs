namespace MyChatApp
{
    using Microsoft.AspNetCore.SignalR.Client;

    internal class ChatService
    {
        public static async Task<HubConnection> Connect()
        {
            var connection = new HubConnectionBuilder()
            .WithAutomaticReconnect()
            .WithUrl("https://53c5-50-106-20-36.ngrok.io/chat")
            .Build();

            await connection.StartAsync();

            return connection;
        }
    }
}
