using CommunityToolkit.Mvvm.Input;
using Contract_Werwolf;
using Werwolf.Window;

namespace Werwolf.MultiplayerMenu;

public class MultiplayerMenuViewModel : ViewModelBase
{
    public MainViewModel MainViewModel { get; set; }
    public RoomClient RoomClient { get; set; }
    public string SignalRId { get; set; }
    public RelayCommand CreateRoomCommand { get; set; }

    public MultiplayerMenuViewModel(MainViewModel mainViewModel, RoomClient roomClient)
    {
        RoomClient = roomClient;
        MainViewModel = mainViewModel;
        CreateRoomCommand = new RelayCommand(CreateRoom);
    }

    private string CreateRoomCode()
    {
        Random random = new Random();
        return random.Next(100000, 1000000).ToString();
    }    
    private bool CheckIfRoomCodeExists(List<RoomDto> rooms, string code)
    {
        return rooms.FirstOrDefault(r => r.RoomCode == code) != null;
    }
    
    private async void CreateRoom()
    {
        //CreateRoomCode
        
        var rooms = await RoomClient.GetAllRooms();

        var code = CreateRoomCode();
        while (CheckIfRoomCodeExists(rooms, code))
        {
            code = CreateRoomCode();
        }
 
        //CreateRoom
        var room = new RoomDto() { RoomCode = code };
        var roomReturned = await RoomClient.PostRoomAsync(room);
        
        //AddPlayer
        await RoomClient.AddPlayer(roomReturned, "Thomas" + "-" + SignalRId);
        
        MainViewModel.GoToLobbyMenu();
    }
}