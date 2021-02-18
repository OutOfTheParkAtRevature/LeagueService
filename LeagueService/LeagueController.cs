using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DataTransfer;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeagueService
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, League Manager, Head Coach, Assistant Coach, Parent, Player")]
    public class LeagueController : ControllerBase
    {
        private readonly Logic _logic;

        public LeagueController(Logic logic)
        {
            _logic = logic;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeague(CreateLeagueDto cld)
        {
            if (await _logic.LeagueExists(cld.LeagueName) == true) return Conflict("League already exists.");
            return Ok(await _logic.AddLeague(cld));
        }

        [HttpPost("team/create")]
        public async Task<IActionResult> CreateTeam(Team team)
        {
            if (await _logic.TeamExists(team.Name) == true) return Conflict("Team already exists.");
            return Ok(await _logic.AddTeam(team));
        }

        [HttpGet("team/{id}")]
        public async Task<IActionResult> GetTeamById(Guid id)
        {
            if (await _logic.TeamExists(id) == false) return NotFound("Team not found");
            return Ok(await _logic.GetTeamById(id));
        }

        [HttpGet("team/{name}")]
        public async Task<IActionResult> GetTeamByName(Guid id)
        {
            if (await _logic.TeamExists(id) == false) return NotFound("Team not found");
            return Ok(await _logic.GetTeamById(id));
        }

        [HttpGet("vendor")]
        public async Task<IActionResult> GetVendors()
        {
            return Ok(await _logic.GetVendors());
        }


    }
}
