using Contract_Werwolf;
using Microsoft.AspNetCore.SignalR.Client;
using Werwolf.MultiplayerMenu;
using Werwolf.Window;

namespace Werwolf.Hub;

public class HubService
{
    private readonly HubConnection hubConnection;
    private readonly MainViewModel _mainViewModel;
    private readonly Random _random;
    private readonly MultiplayerMenuViewModel _multiplayerMenuViewModel;

    public HubService(MainViewModel mainViewModel, MultiplayerMenuViewModel multiplayerMenuViewModel)
    {
        _random = new Random();
        _multiplayerMenuViewModel = multiplayerMenuViewModel;
        _mainViewModel = mainViewModel;
        hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5196/myHub").Build();
        InitializeSignalR();
    }

    private async void InitializeSignalR()
    {
        hubConnection.On<string>("GetRoom", roomid =>
        {
            _mainViewModel.GetRoom(int.Parse(roomid));
        });

        try
        {
            await hubConnection.StartAsync();
            Console.WriteLine("Verbindung hergestellt.");
            var signalRUserId = hubConnection.ConnectionId;
            _mainViewModel.MultiplayerMenuViewModel.SignalRId = signalRUserId;

            _mainViewModel.Player = new PlayerDto { Name = "Thomas" + _random.Next(100000, 1000000), ConnectionId = signalRUserId };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Verbinden: {ex.Message}");
        }
    }
}