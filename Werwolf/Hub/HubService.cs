using Contract_Werwolf;
using Microsoft.AspNetCore.SignalR.Client;
using Werwolf.MultiplayerMenu;
using Werwolf.Window;

namespace Werwolf.Hub;

public class HubService
{
    private readonly HubConnection _hubConnection;
    private readonly MainViewModel _mainViewModel;
    private readonly MultiplayerMenuViewModel _multiplayerMenuViewModel;

    public HubService(MainViewModel mainViewModel, MultiplayerMenuViewModel multiplayerMenuViewModel)
    {
        _multiplayerMenuViewModel = multiplayerMenuViewModel;
        _mainViewModel = mainViewModel;
        _hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5196/myHub").Build();
        InitializeSignalR();
    }

    private async void InitializeSignalR()
    {
        _hubConnection.On<string>("GetAllRooms", message =>
        {
            _mainViewModel.GetAllRooms();
        });

        try
        {
            await _hubConnection.StartAsync();
            Console.WriteLine("Verbindung hergestellt.");
            var signalRUserId = _hubConnection.ConnectionId;
            _mainViewModel.MultiplayerMenuViewModel.SignalRId = signalRUserId;

            Random random = new Random();
            
            _mainViewModel.Player = new PlayerDto { Name = "Thomas" + random.Next(100000, 1000000), ConnectionId = signalRUserId };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Verbinden: {ex.Message}");
        }
    }
}