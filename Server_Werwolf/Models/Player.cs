using System.Collections.ObjectModel;

namespace Server_Werwolf.Models;

public class Player
{
    public long Id { get; set; }
    public string ConnectionId { get; set; }
    public long RoomId { get; set; }
    public string Name { get; set; }
    public Role PlayerRole { get; set; }
}