using CommunityToolkit.Mvvm.Input;
using Contract_Werwolf;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Werwolf.Window;

namespace Werwolf.CodeInputField;

public class CodeInputViewModel : ViewModelBase
{
    private bool _joinButtonIsEnabled;
    public string RoomCode { get; set; }
    public IRoomClient RoomClient { get; }
    public RelayCommand CloseGiveNameCommand { get; }
    public RelayCommand JoinRoomCommand { get; }
    private MainViewModel MainViewModel { get; }

    public CodeInputViewModel(MainViewModel mainViewModel, IRoomClient roomClient)
    {
        MainViewModel = mainViewModel;
        RoomClient = roomClient;
        CloseGiveNameCommand = new RelayCommand(CloseGiveNameMenu);
        JoinRoomCommand = new RelayCommand(JoinRoom);
    }

    private void CloseGiveNameMenu()
    {
        MainViewModel.CodeInputVisible = false;
    }

    private async void JoinRoom()
    {
        MainViewModel.GetRoom(0);

        var gettedRooms = await RoomClient.GetAllRooms();
        var rooms = JsonConvert.DeserializeObject<List<RoomDto>>(gettedRooms);
        if (rooms.Count == 0) return;

        var room = rooms.FirstOrDefault(r => r.RoomCode == RoomCode);
        if (room != null)
        {
            var playerInformation = MainViewModel.Player.Name + "-" + MainViewModel.MultiplayerMenuViewModel.SignalRId;
            await MainViewModel.RoomClient.AddPlayer(room, playerInformation);
            MainViewModel.GoToLobbyMenu();
        }
    }

    public bool JoinButtonIsEnabled
    {
        get => _joinButtonIsEnabled;
        set
        {
            if (_joinButtonIsEnabled != value)
            {
                _joinButtonIsEnabled = value;
                OnPropertyChanged();
            }
        }
    }
}