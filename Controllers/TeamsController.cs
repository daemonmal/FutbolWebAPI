using Microsoft.AspNetCore.Mvc;

namespace FutbolWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        Teams teamsObj = new Teams();

        [HttpGet]
        [Route("/teams/allteams")]
        public IActionResult GetAllTeams()
        {
            try
            {
                return Ok(teamsObj.GetAllTeams());
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        [HttpGet]
        [Route("/teams/")]
        public IActionResult GetTeamByName(string teamName)
        {
            try
            {
                //return Ok(teamsObj.GetTeam(teamName));
                return Ok(teamsObj.GetTeamByName(teamName));
            }
            catch (Exception)
            {
                return BadRequest();
                throw;

            }
        }

        [HttpPost]
        [Route("/teams/addteam/{teamname}")]
        public IActionResult AddTeam(Teams newTeam)
        {
            try
            {
                return Created("", teamsObj.AddNewTeam(newTeam));
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }
    }
}
