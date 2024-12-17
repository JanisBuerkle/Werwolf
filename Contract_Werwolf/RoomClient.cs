using System.Text;
using Newtonsoft.Json;

namespace Contract_Werwolf;

public class RoomClient : IRoomClient
{
    public async void StartRoom(RoomDto room)
    {
        var httpClient = new HttpClient();
        var jsonContent = JsonConvert.SerializeObject(room);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var addPlayerUrl = $"http://localhost:5000/api/Rooms/startroom/{room.Id}";

        var response = await httpClient.PutAsync(addPlayerUrl, httpContent);
        response.EnsureSuccessStatusCode();
    }
    
    public async Task ResetRoom(string playername, RoomDto roomToUpdate)
    {
        var httpClient = new HttpClient();
        var jsonContent = JsonConvert.SerializeObject(roomToUpdate);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var addPlayerUrl = $"http://localhost:5000/api/Rooms/resetroom/{playername}";

        var response = await httpClient.PutAsync(addPlayerUrl, httpContent);
        response.EnsureSuccessStatusCode();
    }

    public async Task AddPlayer(RoomDto roomToUpdate, string playerInformations)
    {
        var httpClient = new HttpClient();
        var jsonContent = JsonConvert.SerializeObject(roomToUpdate);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var addPlayerUrl = $"http://localhost:5000/api/Rooms/addPlayer/{playerInformations}";

        var response = await httpClient.PutAsync(addPlayerUrl, httpContent);
        response.EnsureSuccessStatusCode();
    }
    
    public async Task RemovePlayer(RoomDto roomToUpdate, int id)
    {
        await GetPlayers();

        var httpClient = new HttpClient();
        var jsonContent = JsonConvert.SerializeObject(roomToUpdate);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var addPlayerUrl = $"http://localhost:5000/api/Rooms/removePlayer/{id}";

        var response2 = await httpClient.PutAsync(addPlayerUrl, httpContent);
        response2.EnsureSuccessStatusCode();
        
        var removePlayerUrl = $"http://localhost:5000/api/Player/{id}";

        var response = await httpClient.DeleteAsync(removePlayerUrl);
        response.EnsureSuccessStatusCode();
    }

    public async Task<RoomDto> PostRoomAsync(RoomDto room)
    {
        var httpClient = new HttpClient();
        var jsonContent = JsonConvert.SerializeObject(room);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync("http://localhost:5000/api/Rooms", httpContent);
        if (response.IsSuccessStatusCode)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RoomDto>(responseString);
        }

        return room;
    }

    public async Task<string> GetAllRooms()
    {
        var httpClient = new HttpClient();
        var respone = await httpClient.GetAsync("http://localhost:5000/api/Rooms");
        respone.EnsureSuccessStatusCode();

        var gettedRooms = await respone.Content.ReadAsStringAsync();
        return gettedRooms;
    }
    
    private async Task GetPlayers()
    {
        var httpClient = new HttpClient();
        var respone = await httpClient.GetAsync("http://localhost:5000/api/Player");
        respone.EnsureSuccessStatusCode();
    }
}