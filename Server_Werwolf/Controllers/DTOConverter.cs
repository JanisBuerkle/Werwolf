using System.Collections.ObjectModel;
using Contract_Werwolf;
using Server_Werwolf.Models;

namespace Server_Werwolf.Controllers;

public class DtoConverter
{
    public Room DtoConverterMethod(RoomDto roomDto)
    {
        var room = new Room
        {
            Id = roomDto.Id,
            RoomCode = roomDto.RoomCode,
            OnlineUsersNumber = roomDto.OnlineUsersNumber,
        };

        foreach (var player in roomDto.Players)
        {
            var newRole = new Role() { Id = player.PlayerRole.Id, Name = player.PlayerRole.Name, Team = player.PlayerRole.Team };
            var newPlayer = new Player { Id = player.Id, Name = player.Name, RoomId = player.RoomId, ConnectionId = player.ConnectionId, PlayerRole = newRole };
            room.Players.Add(newPlayer);
        }

        foreach (var role in roomDto.Roles)
        {
            var newCard = new Role { Id = role.Id, Name = role.Name, Team = role.Team };
            room.Roles.Add(newCard);
        }

        return room;
    }
}