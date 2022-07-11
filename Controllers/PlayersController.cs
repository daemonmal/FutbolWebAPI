using Microsoft.AspNetCore.Mvc;

namespace FutbolWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        Players playerObj = new Players();

        [HttpGet]
        [Route("/players/playerName")]
        public IActionResult GetPlayerByName(string playerName)
        {
            try
            {
                return Ok(playerObj.GetPlayerByName(playerName));
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        [HttpGet]
        [Route("/players/position")]
        public IActionResult GetPlayersByPoistion(string positionName)
        {
            try
            {
                return Ok(playerObj.GetPlayersByPosition(positionName));
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        [HttpPost]
        [Route("/players/addplayer")]
        public IActionResult AddPlayer(Players newPlayer)
        {
            try
            {
                return Created("", playerObj.AddPlayer(newPlayer));
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex);
                throw;
            }
        }

        [HttpPut]
        [Route("/players/changeteam")]
        public IActionResult ChangePlayerTeam(string playerName, string newTeamName)
        {
            try
            {
                return Created("", playerObj.ChangePlayerTeam(playerName, newTeamName));
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        [HttpDelete]
        [Route("/players/deleteplayer")]
        public IActionResult DeletePlayer(string playerName)
        {
            try
            {
                return Accepted(playerObj.DeletePlayer(playerName));
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }
    }
}
