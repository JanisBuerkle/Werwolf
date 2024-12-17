﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server_Werwolf.Hubs;
using Server_Werwolf.Models;
using Contract_Werwolf;
using Server_Werwolf.Controllers;

namespace Server_Werwolf;


[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly MyHub myHub;
    private readonly RoomContext context;
    private readonly DtoConverter dtoConverter;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private readonly Random random = new();

    public RoomsController(RoomContext context, MyHub myHub)
    {
        this.myHub = myHub;
        this.context = context;
        dtoConverter = new DtoConverter();
    }

    // GET: api/API
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
    {
        if (context.RoomItems == null)
        {
            return NotFound();
        }

        var allRooms = await context.RoomItems.Include(item => item.Players).ThenInclude(item => item.PlayerRole)
            .Include(item => item.Roles).ToListAsync();

        return allRooms;
    }

    // GET: api/API/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Room>> GetRoom(long id)
    {
        if (context.RoomItems == null)
        {
            return NotFound();
        }

        var roomItem = await context.RoomItems.FindAsync(id);

        if (roomItem == null)
        {
            return NotFound();
        }

        return roomItem;
    }

    // PUT: api/API/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRoom(long id, RoomDto roomItem)
    {
        if (id != roomItem.Id)
        {
            return BadRequest();
        }

        var room = context.RoomItems.Include(r => r.Roles).Include(room => room.Players)
            .First(r => r.Id.Equals(roomItem.Id));

        foreach (var player in room.Players)
        {
            var existingPlayer = await context.Players.FindAsync(player.Id);
            if (existingPlayer != null)
            {
                // Update existing player
                context.Entry(existingPlayer).CurrentValues.SetValues(roomItem);
                context.Entry(existingPlayer).State = EntityState.Modified;
            }
            else
            {
                // Add new player
                context.Players.Add(player);
            }
        }

        context.RoomItems.Update(room);

        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RoomExists(id))
            {
                return NotFound();
            }
        }

        await myHub.SendGetRoom((int)roomItem.Id);
        return NoContent();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [HttpPut("startroom/{roomid}")]
    public async Task<IActionResult> RoomId(long roomid, RoomDto roomItem)
    {
        var room = context.RoomItems.Include(r => r.Roles).Include(room => room.Players).First(r => r.Id.Equals(roomItem.Id));
        
        //ToDo: Rollenvergabe, Start

        context.RoomItems.Update(room);
        await context.SaveChangesAsync();
        await myHub.SendGetRoom((int)roomItem.Id);

        return NoContent();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [HttpPut("resetroom/{playerName}")]
    public async Task<IActionResult> ResetRoom(string playerName, RoomDto roomItem)
    {
        var room = context.RoomItems.Include(r => r.Roles).First(r => r.Id.Equals(roomItem.Id));
        
        room.Roles.Clear();

        context.RoomItems.Update(room);
        await context.SaveChangesAsync();
        await myHub.SendGetRoom((int)roomItem.Id);

        return NoContent();
    }

    [HttpPut("addPlayer/{playerInformations}")]
    public async Task<IActionResult> AddPlayerToRoom(string playerInformations, RoomDto roomItem)
    {
        var p = playerInformations.Split("-");
        var playerName = p[0];
        var connectionId = p[1];
        var player = context.Players.FirstOrDefault(p => p.Name.Equals(playerName));
        var room = context.RoomItems.Include(r => r.Roles).Include(room => room.Players).First(r => r.Id.Equals(roomItem.Id));

        if (player == null)
        {
            player = (await context.Players.AddAsync(new Player { Name = playerName, ConnectionId = connectionId, RoomId = room.Id})).Entity;
        }

        if (room.Players.Count >= 2)
        {
            room.StartButtonEnabled = true;
        }

        if (room.OnlineUsersNumber != 16)
        {
            room.Players.Add(player);
            room.OnlineUsersNumber++;
        }

        context.RoomItems.Update(room);
        await context.SaveChangesAsync();
        await myHub.SendGetRoom((int)roomItem.Id);

        return NoContent();
    }

    [HttpPut("removePlayer/{id}")]
    public async Task<IActionResult> RemovePlayerFromRoom(string id, RoomDto roomItem)
    {
        foreach (var roomFromList in context.RoomItems.Include(r => r.Players))
        {
            foreach (var player in roomFromList.Players)
            {
                if (player.ConnectionId == id)
                {
                    var room = context.RoomItems.Include(r => r.Roles).Include(r => r.Players).First(r => r.Id.Equals(roomFromList.Id));
                    if (room.Players.Count <= 2)
                    {
                        room.StartButtonEnabled = false;
                    }

                    room.OnlineUsersNumber--;
                    
                    var httpClient = new HttpClient();
                    var removePlayerUrl = $"http://localhost:5000/api/Player/{player.Id}";
                    var response = await httpClient.DeleteAsync(removePlayerUrl);

                    context.RoomItems.Update(room);
                    await myHub.SendGetRoom((int)roomItem.Id);
                    await context.SaveChangesAsync();
                }
            }
        }

        return NoContent();
    }

    // POST: api/API
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<RoomDto>> PostRoom(RoomDto roomDto)
    {
        var room = dtoConverter.DtoConverterMethod(roomDto);

        context.RoomItems.Add(room);

        await context.SaveChangesAsync();
        await myHub.SendGetRoom((int)roomDto.Id);
        return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
    }

    // DELETE: api/API/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRoom(long id)
    {
        var roomItem = await context.RoomItems.FindAsync(id);
        if (roomItem == null)
        {
            return NotFound();
        }

        context.RoomItems.Remove(roomItem);
        await context.SaveChangesAsync();
        await myHub.SendGetRoom((int)roomItem.Id);
        return NoContent();
    }

    private bool RoomExists(long id)
    {
        return (context.RoomItems?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}