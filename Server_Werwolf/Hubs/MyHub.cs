using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace Server_Werwolf.Hubs;

public class MyHub : Hub
{
    public async Task SendGetAllRooms()
    {
        await Clients.All.SendAsync("GetAllRooms", "");
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var httpClient = new HttpClient();
        var json = JsonSerializer.Serialize(Context.ConnectionId);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync($"http://localhost:5196/api/Rooms/removePlayer/{Context.ConnectionId}", content);
        response.EnsureSuccessStatusCode();
    
        await base.OnDisconnectedAsync(exception);
    }
}