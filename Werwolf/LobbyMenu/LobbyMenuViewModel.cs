using CommunityToolkit.Mvvm.Input;
using Contract_Werwolf;
using Werwolf.Window;

namespace Werwolf.LobbyMenu;

public class LobbyMenuViewModel : ViewModelBase
{
    public MainViewModel MainViewModel { get; set; }
    public RelayCommand StartRoomCommand { get; set; }
    
    public LobbyMenuViewModel(MainViewModel mainViewModel)
    {
        MainViewModel = mainViewModel;
        StartRoomCommand = new RelayCommand(StartRoom);
    }

    public void StartRoom()
    {
        
    }
}