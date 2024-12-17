using System.Collections.ObjectModel;

namespace Server_Werwolf.Models;

public class Room
{
    public long Id { get; set; }
    public string RoomCode { get; set; }
    public int OnlineUsersNumber { get; set; }
    public bool StartButtonEnabled { get; set; }
    public List<Player> Players { get; set; } = new();
    public ObservableCollection<Role> Roles { get; set; } = new();
}