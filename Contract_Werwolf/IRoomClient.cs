namespace Contract_Werwolf;

public interface IRoomClient
{
    Task<string> GetAllRooms();
    Task AddPlayer(RoomDto roomToUpdate, string playerInformations);
    void StartRoom(RoomDto room);
    Task ResetRoom(string playername, RoomDto roomToUpdate);
    Task RemovePlayer(RoomDto roomToUpdate, int id);
    Task<RoomDto> PostRoomAsync(RoomDto room);
}