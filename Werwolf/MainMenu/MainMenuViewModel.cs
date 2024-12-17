using CommunityToolkit.Mvvm.Input;
using Werwolf.Window;

namespace Werwolf.MainMenu;

public class MainMenuViewModel : ViewModelBase
{
    public MainViewModel MainViewModel { get; set; }
    public RelayCommand PlayButtonCommand { get; set; }
    public RelayCommand OpenInventoryCommand { get; set; }
    public RelayCommand OpenShopCommand { get; set; }
    public MainMenuViewModel(MainViewModel mainViewModel)
    {
        MainViewModel = mainViewModel;
        PlayButtonCommand = new RelayCommand(OpenMultiplayerMenu);
        OpenInventoryCommand = new RelayCommand(OpenInventoryMenu);
        OpenShopCommand = new RelayCommand(OpenShopMenu);
    }

    private void OpenMultiplayerMenu()
    {
        MainViewModel.GoToMultiplayerMenu();
    }
    
    private void OpenInventoryMenu()
    {
        // MainViewModel.GoToInventoryMenu();
    }
    
    private void OpenShopMenu()
    {
        // MainViewModel.GoToShopMenu();
    }
}