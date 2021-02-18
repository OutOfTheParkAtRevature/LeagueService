using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Model.DataTransfer;
using Repository;

namespace Service
{
    public class Logic
    {
        public Logic() { }
       
        public Logic(Repo repo, ILogger<Repo> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        private readonly Repo _repo;
        private readonly ILogger<Repo> _logger;

        /// <summary>
        /// Get list of Teams
        /// </summary>
        /// <returns>Teams</returns>
        public async Task<IEnumerable<Team>> GetTeams()
        {
            return await _repo.GetTeams();
        }

        /// <summary>
        /// Get Team by ID
        /// </summary>
        /// <param name="id">TeamID</param>
        /// <returns>Team</returns>
        public async Task<Team> GetTeamById(Guid id)
        {
            return await _repo.GetTeamById(id);
        }

        /// <summary>
        /// Get Team by Name
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        public async Task<Team> GetTeamByName(string teamName)
        {
            return await _repo.GetTeamByName(teamName);
        }

        /// <summary>
        /// Gets list of all vendors
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Vendor>> GetVendors()
        {
            return await _repo.GetVendors();
        }
        
        /// <summary>
        /// Get Vendor By Id
        /// </summary>
        /// <param name="leagueName"></param>
        /// <returns></returns>
        /// 
        public async Task<Vendor> GetVendorById(Guid id)
        {
            return await _repo.GetVendorById(id);
        }

        /// <summary>
        /// Get Vendor by name
        /// </summary>
        /// <param name="vendorName"></param>
        /// <returns></returns>
        public async Task<Vendor> GetVendorByName(string vendorName)
        {
            return await _repo.GetVendorByName(vendorName);
        }


        public async Task<bool> LeagueExists(string leagueName)
        {
            bool leagueExists = await _repo.Leagues.AnyAsync(x => x.LeagueName == leagueName);
            if (leagueExists)
            {
                _logger.LogInformation("League found in database");
                return leagueExists;
            }
            return leagueExists;

        }
        public async Task<bool> SportExists(string sportName)
        {
            bool sportExists = await _repo.Sports.AnyAsync(x => x.SportName == sportName);
            if (sportExists)
            {
                _logger.LogInformation("Sport found in database");
                return sportExists;
            }
            return sportExists;
        }

        public async Task<bool> TeamExists(Guid teamId)
        {
            bool teamExists = await _repo.Teams.AnyAsync(x => x.TeamID == teamId);
            if (teamExists)
            {
                _logger.LogInformation("Sport found in database");
                return teamExists;
            }
            return teamExists;
        }

        public async Task<bool> TeamExists(string teamName)
        {
            bool teamExists = await _repo.Teams.AnyAsync(x => x.Name == teamName);
            if (teamExists)
            {
                _logger.LogInformation("Sport found in database");
                return teamExists;
            }
            return teamExists;
        }

        public async Task<League> AddLeague(CreateLeagueDto cld)
        {
            Sport sport = new Sport { SportName = cld.SportName };
            if (await SportExists(cld.SportName) == false)
            {
                _repo.Sports.Add(sport);
                await _repo.CommitSave();
            }
            
            if(await LeagueExists(cld.LeagueName) == false)
            {
                League league = new League
                {
                    LeagueName = cld.LeagueName,
                    SportID = sport.SportID,
                };
                _repo.Leagues.Add(league);
                await _repo.CommitSave();
                return league;
            }
            return null;
        }

        public async Task<Team> AddTeam(Team team)
        {
            ///create carpoolreceipientlist add id to team
            if(await TeamExists(team.Name) == false) { 
                _repo.Teams.Add(team);
                await _repo.CommitSave();
                return team;
            }
            return null;
        }

        public async Task<Vendor> AddVendor(string vendorName, string vendorInfo)
        {
            Vendor newVendor = new Vendor()
            {
                VendorID = Guid.NewGuid(),
                VendorName = vendorName,
                VendorInfo = vendorInfo
            };
            await _repo.Vendors.AddAsync(newVendor);
            await _repo.CommitSave();
            return newVendor;
        }

        /// <summary>
        /// Edit Team
        /// </summary>
        /// <param name="id">Team to edit</param>
        /// <param name="editTeamDto">New information</param>
        /// <returns>modified Team</returns>
        public async Task<Team> EditTeam(Guid id, EditTeamDto editTeamDto)
        {
            Team tTeam = await GetTeamById(id);
            if (tTeam.Name != editTeamDto.Name && editTeamDto.Name != "") { tTeam.Name = editTeamDto.Name; }
            if (tTeam.Wins != editTeamDto.Wins && editTeamDto.Wins >= 0) { tTeam.Wins = editTeamDto.Wins; }
            if (tTeam.Losses != editTeamDto.Losses && editTeamDto.Losses >= 0) { tTeam.Losses = editTeamDto.Losses; }
            await _repo.CommitSave();
            return tTeam;
        }

        //public async Task<League> EditLeagueName(int id, string leagueName)
        //{
        //    League league = await 
        //    if (tTeam.Name != editTeamDto.Name && editTeamDto.Name != "") { tTeam.Name = editTeamDto.Name; }
        //}


    }
}