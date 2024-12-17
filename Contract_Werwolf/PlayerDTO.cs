namespace Contract_Werwolf;

public class PlayerDto
{
    public long Id { get; set; }
    public string ConnectionId { get; set; }
    public long RoomId { get; set; }
    public string Name { get; set; }
    public RoleDto PlayerRole { get; set; }
}