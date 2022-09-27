using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<ChatHub>("/chat");
app.Run();

public class ChatHub : Hub
{
    public Task SendMessage(string message)
    {
        return Clients.All.SendAsync("messageReceived", message);
    }
}
