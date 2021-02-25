using Microsoft.AspNetCore.Authentication;
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
    public class LeagueController : ControllerBase
    {
        private readonly Logic _logic;

        public LeagueController(Logic logic)
        {
            _logic = logic;
        }

        [HttpGet]
        public async Task<IActionResult> GetLeagues()
        {
            return Ok(await _logic.GetLeagues());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeagueById(Guid id)
        {
            if (await _logic.LeagueExistsID(id) == false) return NotFound("League not found.");
            return Ok(await _logic.GetLeagueById(id));
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateLeague([FromBody] CreateLeagueDto cld)
        {
            if (await _logic.LeagueExists(cld.LeagueName) == true) return Conflict("League with that name already exists.");
            return Ok(await _logic.AddLeague(cld));
        }
        

        [HttpPut("{id}")]
        public async Task<IActionResult> EditLeague(Guid id, [FromBody] string leagueName)
        {
            if (await _logic.LeagueExistsID(id) == false) return NotFound("League with that ID not found.");
            return Ok(await _logic.EditLeague(id, leagueName));
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeague(Guid id)
        {
            League league = await _logic.GetLeagueById(id);
            if (league == null) return NotFound("League with that ID not found.");
            return Ok(await _logic.DeleteLeague(league));

        }
        

        


    }
}
