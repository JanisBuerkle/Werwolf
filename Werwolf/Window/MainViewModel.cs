﻿using System.Collections.ObjectModel;
using System.Windows;
using Contract_Werwolf;
using Newtonsoft.Json;
using Werwolf.CodeInputField;
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
    private bool _codeInputVisible;
    private bool _gameMenuVisible;
    private RoomDto _room;

    private string _screenModeSymbol = "\u25a2";

    public PlayerDto Player { get; set; }
    public string RoomCode { get; }
    public HubService HubService { get; }
    public RoomClient RoomClient { get; }
    public MainMenuViewModel MainMenuViewModel { get; }
    public MultiplayerMenuViewModel MultiplayerMenuViewModel { get; set; }
    public LobbyMenuViewModel LobbyMenuViewModel { get; set; }
    public GameMenuViewModel GameMenuViewModel { get; set; }
    public CodeInputViewModel CodeInputViewModel { get; set; }

    public MainViewModel()
    {
        RoomClient = new RoomClient();
        MultiplayerMenuViewModel = new MultiplayerMenuViewModel(this, RoomClient);
        LobbyMenuViewModel = new LobbyMenuViewModel(this);
        GameMenuViewModel = new GameMenuViewModel();
        CodeInputViewModel = new CodeInputViewModel(this, RoomClient);
        MainMenuViewModel = new MainMenuViewModel(this);

        GoToMainMenu();
        // GoToLobbyMenu();
        // GoToGameMenu();
    }

    public void GoToMainMenu()
    {
        MainMenuVisible = true;
        GameMenuVisible = false;
        CodeInputVisible = false;
        LobbyMenuVisible = false;
        MultiplayerMenuVisible = false;
    }

    public void GoToMultiplayerMenu()
    {
        MultiplayerMenuVisible = true;
        MainMenuVisible = false;
        GameMenuVisible = false;
        CodeInputVisible = false;
        LobbyMenuVisible = false;
    }

    public void GoToLobbyMenu()
    {
        LobbyMenuVisible = true;
        MainMenuVisible = false;
        GameMenuVisible = false;
        CodeInputVisible = false;
        MultiplayerMenuVisible = false;
    }

    public void GoToGameMenu()
    {
        GameMenuVisible = true;
        MainMenuVisible = false;
        LobbyMenuVisible = false;
        CodeInputVisible = false;
        MultiplayerMenuVisible = false;
    }

    public void OpenCodeInputField()
    {
        CodeInputVisible = true;
    }

    public void AddPlayer()
    {
    }

    public async Task GetAllRooms()
    {
        var gettedRoomsTask = RoomClient.GetAllRooms();
        var gettedRooms = await gettedRoomsTask;

        var rooms = JsonConvert.DeserializeObject<List<RoomDto>>(gettedRooms);

        if (Room != null)
        {
            foreach (var room in rooms)
            {
                if (Room.Id == room.Id)
                {
                    Room = room;
                }
            }

            foreach (var player in Room.Players)
            {
                if (Player.Name == player.Name)
                {
                    Player = player;
                }
            }
            
            foreach (var room in rooms)
            {
                if (room.OnlineUsersNumber >= 16)
                {
                    //Raum ist voll irgendwie sagen
                }
            }
        }
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

    public bool CodeInputVisible
    {
        get => _codeInputVisible;
        set
        {
            if (value == _codeInputVisible)
            {
                return;
            }

            _codeInputVisible = value;
            OnPropertyChanged();
        }
    }

    public RoomDto Room
    {
        get => _room;
        set
        {
            if (_room != value)
            {
                _room = value;
                OnPropertyChanged();
            }
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