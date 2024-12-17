using Werwolf.GameMenu;
using Werwolf.Hub;
using Werwolf.LobbyMenu;
using Werwolf.MainMenu;
using Werwolf.MultiplayerMenu;

namespace Werwolf.Window;

public class MainViewModel : ViewModelBase
{
    private bool _mainMenuVisible;
    private bool _multiplayerMenuVisible;
    private bool _lobbyMenuVisible;
    private bool _gameMenuVisible;

    private string _screenModeSymbol = "\u25a2";

    public HubService HubService { get; }
    public MainMenuViewModel MainMenuViewModel { get; }
    public MultiplayerMenuViewModel MultiplayerMenuViewModel { get; set; }
    public LobbyMenuViewModel LobbyMenuViewModel { get; set; }
    public GameMenuViewModel GameMenuViewModel { get; set; }

    public MainViewModel()
    {
        MultiplayerMenuViewModel = new MultiplayerMenuViewModel();
        HubService = new HubService(this, MultiplayerMenuViewModel);
        LobbyMenuViewModel = new LobbyMenuViewModel();
        GameMenuViewModel = new GameMenuViewModel();
        MainMenuViewModel = new MainMenuViewModel();

        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        _mainMenuVisible = true;
        _multiplayerMenuVisible = false;
        _lobbyMenuVisible = false;
        _gameMenuVisible = false;
    }

    public void GoToMultiplayerMenu()
    {
        _mainMenuVisible = false;
        _multiplayerMenuVisible = true;
        _lobbyMenuVisible = false;
        _gameMenuVisible = false;
    }

    public void GoToLobbyMenu()
    {
        _mainMenuVisible = false;
        _multiplayerMenuVisible = false;
        _lobbyMenuVisible = true;
        _gameMenuVisible = false;
    }

    public void GoToGameMenu()
    {
        _mainMenuVisible = false;
        _multiplayerMenuVisible = false;
        _lobbyMenuVisible = false;
        _gameMenuVisible = true;
    }

    public bool MainMenuVisible
    {
        get => _mainMenuVisible;
        set
        {
            if (value == _mainMenuVisible)
            {
                return;
            }

            _mainMenuVisible = value;
            OnPropertyChanged();
        }
    }

    public bool MultiplayerMenuVisible
    {
        get => _multiplayerMenuVisible;
        set
        {
            if (value == _multiplayerMenuVisible)
            {
                return;
            }

            _multiplayerMenuVisible = value;
            OnPropertyChanged();
        }
    }

    public bool LobbyMenuVisible
    {
        get => _lobbyMenuVisible;
        set
        {
            if (value == _lobbyMenuVisible)
            {
                return;
            }

            _lobbyMenuVisible = value;
            OnPropertyChanged();
        }
    }

    public bool GameMenuVisible
    {
        get => _gameMenuVisible;
        set
        {
            if (value == _gameMenuVisible)
            {
                return;
            }

            _gameMenuVisible = value;
            OnPropertyChanged();
        }
    }

    public string ScreenModeSymbol
    {
        get => _screenModeSymbol;
        set
        {
            if (_screenModeSymbol != value)
            {
                _screenModeSymbol = value;
                OnPropertyChanged();
            }
        }
    }
}