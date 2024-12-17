using System.Collections.ObjectModel;

namespace Contract_Werwolf;

public class RoomDto
{
    public long Id { get; set; }
    public string RoomCode { get; set; }
    public int OnlineUsersNumber { get; set; }
    public bool StartButtonEnabled { get; set; }
    public List<PlayerDto> Players { get; set; } = new();
    public ObservableCollection<RoleDto> Roles { get; set; } = new();
}