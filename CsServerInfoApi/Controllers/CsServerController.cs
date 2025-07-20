using Microsoft.AspNetCore.Mvc;
using SteamQueryNet;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CsServerInfoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CsServerController : ControllerBase
    {
        [HttpGet("info")]
        public async Task<IActionResult> GetServerInfo([FromQuery] string ip, [FromQuery] ushort port)
        {
            var server = new ServerQuery(ip, port);
            try
            {
                var info = await server.GetServerInfoAsync();
                var players = await server.GetPlayersAsync();

                return Ok(new
                {
                    ServerName = info.Name,
                    Map = info.Map,
                    Players = info.Players,
                    MaxPlayers = info.MaxPlayers,
                    Game = info.Game,
                    PlayerList = players.Select(p => new
                    {
                        p.Name,
                        p.Score,
                        Duration = Math.Round(p.Duration)
                    })
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}