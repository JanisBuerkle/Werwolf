using Contract_Werwolf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server_Werwolf.Hubs;
using Server_Werwolf.Models;

namespace UNO_Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly MyHub myHub;
    private readonly RoomContext context;

    public PlayerController(RoomContext context, MyHub myHub)
    {
        this.myHub = myHub;
        this.context = context;
    }

    // GET: api/Player
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
    {
        if (context.Players == null)
        {
            return NotFound();
        }

        return await context.Players.ToListAsync();
    }

    // GET: api/Player/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> GetMultiplayerPlayer(long id)
    {
        if (context.Players == null)
        {
            return NotFound();
        }

        var multiplayerPlayer = await context.Players.FindAsync(id);

        if (multiplayerPlayer == null)
        {
            return NotFound();
        }

        return multiplayerPlayer;
    }

    // PUT: api/Player/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMultiplayerPlayer(long id, PlayerDto playerPlayer)
    {
        if (id != playerPlayer.Id)
        {
            return BadRequest();
        }

        context.Entry(playerPlayer).State = EntityState.Modified;


        try
        {
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MultiplayerPlayerExists(id))
            {
                return NotFound();
            }

            throw;
        }

        return NoContent();
    }

    // POST: api/Player
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<PlayerDto>> PostMultiplayerPlayer(PlayerDto playerPlayer)
    {
        var player = context.Players.First(p => p.Id.Equals(playerPlayer.Id));
        if (context.Players == null)
        {
            return Problem("Entity set 'RoomContext.Players'  is null.");
        }

        context.Players.Add(player);
        await context.SaveChangesAsync();

        return CreatedAtAction("GetMultiplayerPlayer", new { id = player.Id }, player);
    }

    // DELETE: api/Player/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMultiplayerPlayer(long id)
    { 
        if (context.Players == null)
        {
            return NotFound();
        }

        var multiplayerPlayer = await context.Players.FindAsync(id);
        if (multiplayerPlayer == null)
        {
            return NotFound();
        }

        context.Players.Remove(multiplayerPlayer);
        await context.SaveChangesAsync();

        await myHub.SendGetRoom((int)multiplayerPlayer.RoomId);
        return NoContent();
    }

    private bool MultiplayerPlayerExists(long id)
    {
        return (context.Players?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}