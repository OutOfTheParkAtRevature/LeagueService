using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DataTransfer;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LeagueService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamController : ControllerBase
    {

        private readonly Logic _logic;

        public TeamController(Logic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTeams()
        {
            return Ok(await _logic.GetTeams());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamById(Guid id)
        {
            Team team = await _logic.GetTeamById(id);
            if (team == null) return NotFound("Team not found");
            return Ok(team);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetTeamByName(string name)
        {
            Team team = await _logic.GetTeamByName(name);
            if (team == null) return NotFound("League with that ID not found.");
            return Ok(team);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] string teamName)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (await _logic.TeamExists(teamName) == true) return Conflict("Team already exists.");
            return Ok(await _logic.AddTeam(teamName, token));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTeam(Guid id, [FromBody] EditTeamDto etd) {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            if (await _logic.TeamExistsID(id) == false) return NotFound("Team with that ID not found.");
            if (_logic.CanEdit(claimsIdentity, id) == false) return Forbid("Not authorized to edit this team.");
            return Ok(await _logic.EditTeam(id, etd));          
        }

        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(Guid id)
        {
            Team team = await _logic.GetTeamById(id);
            if (team == null) return NotFound("Team with that ID not found.");
            return Ok(await _logic.DeleteTeam(team));
        }
    }
}
