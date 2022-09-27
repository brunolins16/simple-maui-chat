namespace MyChatApp
{
    using Microsoft.AspNetCore.SignalR.Client;

    internal class ChatService
    {
        public static async Task<HubConnection> Connect()
        {
            var connection = new HubConnectionBuilder()
            .WithAutomaticReconnect()
            .WithUrl("https://[myappserver]:[port]/chat")
            .Build();

            await connection.StartAsync();

            return connection;
        }
    }
}
