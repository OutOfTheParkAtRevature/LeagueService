using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using Model.DataTransfer;
using Models;
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


        /*
         * 
         * 
         * League Logic
         * 
         * 
         */

        /// <summary>
        /// Checks to see if a league with the guid exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> LeagueExistsID(Guid id)
        {
            bool leagueExists = await _repo.Leagues.AnyAsync(x => x.LeagueID == id);
            if (leagueExists)
            {
                _logger.LogInformation("League found in database");
                return leagueExists;
            }
            return leagueExists;

        }

        /// <summary>
        /// Checks to see if a league with the name exists in the DB
        /// </summary>
        /// <param name="leagueName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all leagues in the DB
        /// </summary>
        /// <returns></returns>
        public async Task<IList<League>> GetLeagues()
        {
            return await _repo.Leagues.ToListAsync();
        }

        /// <summary>
        /// Gets league with given ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<League> GetLeagueById(Guid id)
        {
            return await _repo.Leagues.FirstOrDefaultAsync(x => x.LeagueID == id);
        }

        /// <summary>
        /// Adds league with league name and sport name
        /// Adds the sport to the db if it doesn't exist
        /// </summary>
        /// <param name="cld"></param>
        /// <returns></returns>
        public async Task<League> AddLeague(CreateLeagueDto cld)
        {
            Sport sport = new Sport { SportName = cld.SportName };
            if (await SportExists(cld.SportName) == false)
            {
                _repo.Sports.Add(sport);
                await _repo.CommitSave();
            }

            if (await LeagueExists(cld.LeagueName) == false)
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

        /// <summary>
        /// Edit the league name of the league with the given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="leagueName"></param>
        /// <returns></returns>
        public async Task<League> EditLeague(Guid id, string leagueName)
        {
            League league = await GetLeagueById(id);
            if (league.LeagueName != leagueName && !string.IsNullOrEmpty(leagueName)) league.LeagueName = leagueName;
            await _repo.CommitSave();
            return league;

        }

        /// <summary>
        /// Remove the given league from the DB
        /// </summary>
        /// <param name="league"></param>
        /// <returns></returns>
        public async Task<bool> DeleteLeague(League league)
        {
            _repo.Leagues.Remove(league);
            await _repo.CommitSave();
            _logger.LogInformation("League removed");
            return true;

        }


        /*
         * 
         * 
         * Team Logic
         * 
         * 
         */

        /// <summary>
        /// Checks to see if a team with the given ID exists in the DB
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public async Task<bool> TeamExistsID(Guid teamId)
        {
            bool teamExists = await _repo.Teams.AnyAsync(x => x.TeamID == teamId);
            if (teamExists)
            {
                _logger.LogInformation("Team found in database");
                return teamExists;
            }
            return teamExists;
        }

        /// <summary>
        /// Checks to see if a team with the given name exists in the DB
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        public async Task<bool> TeamExists(string teamName)
        {
            bool teamExists = await _repo.Teams.AnyAsync(x => x.Name == teamName);
            if (teamExists)
            {
                _logger.LogInformation("Team found in database");
                return teamExists;
            }
            return teamExists;
        }

        /// <summary>
        /// Get list of Teams
        /// </summary>
        /// <returns>Teams</returns>
        public async Task<IEnumerable<Team>> GetTeams()
        {
            await _repo.SeedLeague();
            await _repo.SeedSports();
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

            return await _repo.GetTeamByNameAndLeague(teamName);
        }

        /// <summary>
        /// Creates a new team with the given name, 0 wins/losses and creates a carpool recipient list
        /// Uses bearer token from controller to send a post request to messageservice to create the carpool recipient list
        /// </summary>
        /// <param name="teamName"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Team> AddTeam(string teamName, string token)
        {
            var leagues = await GetLeagues();
            Team team = new Team
            {
                Name = teamName,
                CarpoolID = Guid.NewGuid(),
                LeagueID = leagues[0].LeagueID,
            };
            _repo.Teams.Add(team);
            await _repo.CommitSave();
            return team;
        }

        public bool CanEdit(ClaimsIdentity claimsIdentity, Guid teamId)
        {
            var teamClaim = claimsIdentity.FindFirst(ClaimTypes.GroupSid);
            var roleClaim = claimsIdentity.FindFirst(ClaimTypes.Role);
            string loggedInUserRole = roleClaim.Value;
            string loggedInUserTeamId = teamClaim.Value;
            if (loggedInUserRole != "Head Coach") return true;
            if (loggedInUserTeamId == teamId.ToString()) return true;
            return false;
        }
        /// <summary>
        /// Edit team with given id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="etd"></param>
        /// <returns></returns>
        public async Task<Team> EditTeam(Guid id, EditTeamDto etd)
        {
            Team team = await _repo.GetTeamById(id);
            if (team.Name != etd.Name && !string.IsNullOrEmpty(etd.Name)) team.Name = etd.Name;
            if (team.Wins != etd.Wins && etd.Wins != null) team.Wins = (int)etd.Wins;
            if (team.Losses != etd.Losses && etd.Losses != null) team.Losses = (int)etd.Losses;
            await _repo.CommitSave();
            return team;
        }

        /// <summary>
        /// Delete given team from the DB
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public async Task<bool> DeleteTeam(Team team)
        {
                _repo.Teams.Remove(team);
                await _repo.CommitSave();
                _logger.LogInformation("Team removed");
                return true;
        }


        /*
         * 
         * 
         * Vendor Logic
         * 
         * 
         */

        /// <summary>
        /// Checks to see if a vendor with the given name exists in the DB
        /// </summary>
        /// <param name="vName"></param>
        /// <returns></returns>
        public async Task<bool> VendorExists(string vName)
        {
            bool vendorExists = await _repo.Vendors.AnyAsync(x => x.VendorName == vName);
            if (vendorExists)
            {
                _logger.LogInformation("Team found in database");
                return vendorExists;
            }
            return vendorExists;

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

        /// <summary>
        /// Add a new vendor to the DB with given name and info
        /// </summary>
        /// <param name="createVendorDto"></param>
        /// <returns></returns>
        public async Task<Vendor> AddVendor(CreateVendorDto createVendorDto)
        {
            Vendor newVendor = new Vendor()
            {
                VendorName = createVendorDto.VendorName,
                VendorInfo = createVendorDto.VendorInfo,
            };
            await _repo.Vendors.AddAsync(newVendor);
            await _repo.CommitSave();
            return newVendor;
        }

        /// <summary>
        /// Edit the vendor name/info with the given ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="evd"></param>
        /// <returns></returns>
        public async Task<Vendor> EditVendor(Guid id, CreateVendorDto evd)
        {
            Vendor vendor = await _repo.GetVendorById(id);
            if (vendor.VendorName != evd.VendorName && !string.IsNullOrEmpty(evd.VendorName)) vendor.VendorName = evd.VendorName;
            if (vendor.VendorInfo != evd.VendorInfo && !string.IsNullOrEmpty(evd.VendorInfo)) vendor.VendorInfo = evd.VendorInfo;
            await _repo.CommitSave();
            return vendor;
        }

        /// <summary>
        /// Delete the given vendor from the DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteVendor(Vendor vendor)
        {
            _repo.Vendors.Remove(vendor);
            await _repo.CommitSave();
            _logger.LogInformation("Team removed");
            return true;
        }

        




        /*
         * 
         * 
         * Sport Logic
         * 
         * 
         */

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

        

        

       

        

        

        //public async Task<League> EditLeagueName(League league)
        //{
        //    if (tTeam.Name != editTeamDto.Name && editTeamDto.Name != "") { tTeam.Name = editTeamDto.Name; }
        //}


    }
}