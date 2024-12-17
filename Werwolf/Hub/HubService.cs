using Microsoft.AspNetCore.SignalR.Client;
using Werwolf.MultiplayerMenu;
using Werwolf.Window;

namespace Werwolf.Hub;

public class HubService
{
    private readonly HubConnection hubConnection;
    private readonly MainViewModel _mainViewModel;
    private readonly MultiplayerMenuViewModel _multiplayerMenuViewModel;

    public HubService(MainViewModel mainViewModel, MultiplayerMenuViewModel multiplayerMenuViewModel)
    {
        _multiplayerMenuViewModel = multiplayerMenuViewModel;
        _mainViewModel = mainViewModel;
        hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5196/myHub").Build();
        InitializeSignalR();
    }

    private async void InitializeSignalR()
    {
        hubConnection.On<string>("GetRoom", roomid =>
        {
            
        });

        try
        {
            await hubConnection.StartAsync();
            Console.WriteLine("Verbindung hergestellt.");
            var signalRUserId = hubConnection.ConnectionId;
            _mainViewModel.MultiplayerMenuViewModel.SignalRId = signalRUserId;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Verbinden: {ex.Message}");
        }
    }
}