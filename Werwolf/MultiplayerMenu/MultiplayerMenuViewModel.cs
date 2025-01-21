using CommunityToolkit.Mvvm.Input;
using Contract_Werwolf;
using Newtonsoft.Json;
using Werwolf.Window;

namespace Werwolf.MultiplayerMenu;

public class MultiplayerMenuViewModel : ViewModelBase
{
    public MainViewModel MainViewModel { get; set; }
    public IRoomClient RoomClient { get; set; }
    public string SignalRId { get; set; }
    public RelayCommand CreateRoomCommand { get; set; }
    public RelayCommand JoinRoomCommand { get; set; }

    public MultiplayerMenuViewModel(MainViewModel mainViewModel, IRoomClient roomClient)
    {
        RoomClient = roomClient;
        MainViewModel = mainViewModel;
        CreateRoomCommand = new RelayCommand(CreateRoom);
        JoinRoomCommand = new RelayCommand(JoinRoom);
    }

    private string CreateRoomCode()
    {
        Random random = new Random();
        return random.Next(100000, 1000000).ToString();
    }

    private bool CheckIfRoomCodeExists(List<RoomDto>? rooms, string code)
    {
        return rooms.FirstOrDefault(r => r.RoomCode == code) != null;
    }

    private void JoinRoom()
    {
        MainViewModel.OpenCodeInputField();
    }

    private async void CreateRoom()
    {
        //CreateRoomCode
        var gettedRooms = await RoomClient.GetAllRooms();
        var rooms = JsonConvert.DeserializeObject<List<RoomDto>>(gettedRooms);

        var code = CreateRoomCode();
        while (CheckIfRoomCodeExists(rooms, code))
        {
            code = CreateRoomCode();
        }

        //CreateRoom
        var room = new RoomDto() { RoomCode = code };
        MainViewModel.Room = room;
        var roomReturned = await RoomClient.PostRoomAsync(room);

        //AddPlayer
        await RoomClient.AddPlayer(roomReturned, MainViewModel.Player + "-" + SignalRId);

        MainViewModel.GoToLobbyMenu();
    }
}